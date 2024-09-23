import apiClient from "../Common/authorizedData";
import React, { useEffect, useState } from "react";
import { deleteHandler } from "./common functions/deleteHandler";
import { fetchDataFromApi } from "./common functions/fetchData";
import AddEventTable from "./common functions/EventsHandlers/AddEventTable";
import EventsTable from "./common functions/EventsHandlers/EventsTable";
import JoinedMembers from "./common functions/EventsHandlers/JoinedMembers";
import AvailableGuides from "./common functions/EventsHandlers/AvailableGuides";

const AdminEvents = () => {
  const [events, setEvents] = useState([]); // Ensure it's initialized as an empty array
  const [isEditing, setIsEditing] = useState(null);
  const [isAdding, setIsAdding] = useState(false);
  const [members, setMembers] = useState([]); // State to hold members of an event
  const [showMembersId, setShowMembersId] = useState(null); // Track which event members to display
  const [addingGuideId, setAddingGuideId] = useState(null); // Track which event we're adding a guide to
  const [guideEmail, setGuideEmail] = useState(""); // Input value for the guide email
  const [guideData, setGuideData] = useState([]);
  const [showTable, setShowTable] = useState(false);
  const [editedEvent, setEditedEvent] = useState({
    name: "",
    description: "",
    cost: 0,
    status: "",
    destination: "",
    category: 0,
    dateFrom: "",
    dateTo: "",
  });

  const [newEvent, setNewEvent] = useState({
    name: "",
    description: "",
    cost: 0,
    status: "",
    destination: "",
    category: 0,
    dateFrom: "",
    dateTo: "",
  });

  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Event/GetAllEvents",
      setEvents
    );
  }, [events, guideData]);

  const handleDelete = async (id) => {
    deleteHandler("Event", "id", id, setEvents);
  };

  const handleEdit = (event) => {
    setIsEditing(event.id);
    setEditedEvent({
      name: event.name || "",
      description: event.description || "",
      cost: event.cost || 0,
      status: event.status || "",
      destination: event.destination || "",
      category: event.category || 0,
      dateFrom: event.dateFrom || "",
      dateTo: event.dateTo || "",
    });
  };

  const handleSave = async (id) => {
    try {
      await apiClient.put(
        `https://localhost:7095/api/Event/UpdateEvent?id=${id}`,
        editedEvent
      );
      setEvents(events.map((event) => (event.id === id ? editedEvent : event)));
      setIsEditing(null);
    } catch (error) {
      console.error("Error saving event:", error);
    }
  };

  const handleAdd = async () => {
    try {
      await apiClient.post(
        "https://localhost:7095/api/Event/CreateEvent",
        newEvent
      );
      setIsAdding(false); // Reset after successful add
    } catch (error) {
      console.error("Error adding event:", error);
    } finally {
      setNewEvent({}); // Clear input fields after adding
      setEvents([...events, newEvent]); // Optionally add the new event to the list
    }
  };

  const handleCancel = () => {
    setIsAdding(false);
    setNewEvent({}); // Reset the input fields when cancelled
  };

  const handleToggleMembers = async (id) => {
    if (showMembersId === id) {
      // If members for this event are already shown, hide them
      setShowMembersId(null);
    } else {
      // Fetch and show members for the selected event
      try {
        const response = await apiClient.get(
          `https://localhost:7095/api/Event/GetEventById?id=${id}`
        );
        setMembers(response.data.members || []);
        setShowMembersId(id);
      } catch (error) {
        console.error("Error fetching event members:", error);
      }
    }
  };

  const handleAddGuide = async (id) => {
    try {
      await apiClient.put(
        `https://localhost:7095/api/Event/AddGuide?email=${guideEmail}&id=${id}`
      );
      setAddingGuideId(null); // Hide the input div after successful guide addition
      setGuideEmail(""); // Reset email input
    } catch (error) {
      console.error("Error adding guide:", error);
    }
  };

  const fetchGuideData = async (id) => {
    try {
      const response = await apiClient.get(
        `https://localhost:7095/api/Event/GetAvailableGuides?id=${id}`
      ); // Replace with your URL
      setGuideData(response.data);
      setShowTable(true);
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  };

  return (
    <>
      <EventsTable
        events={events}
        isEditing={isEditing}
        setEditedEvent={setEditedEvent}
        editedEvent={editedEvent}
        handleSave={handleSave}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
        handleToggleMembers={handleToggleMembers}
        showMembersId={showMembersId}
        setAddingGuideId={setAddingGuideId}
        addingGuideId={addingGuideId}
        fetchGuideData={fetchGuideData}
        setGuideEmail={setGuideEmail}
        handleAddGuide={handleAddGuide}
        guideEmail={guideEmail}
      />
      <AddEventTable
        isAdding={isAdding}
        newEvent={newEvent}
        setNewEvent={setNewEvent}
        setIsAdding={setIsAdding}
        handleAdd={handleAdd}
        handleCancel={handleCancel}
      />

      {/* Display members of selected event */}
      <JoinedMembers showMembersId={showMembersId} members={members} />

      <AvailableGuides
        showTable={showTable}
        addingGuideId={addingGuideId}
        guideData={guideData}
      />
    </>
  );
};

export default AdminEvents;
