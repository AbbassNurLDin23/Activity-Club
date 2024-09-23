import React, { useEffect, useState } from "react";
import apiClient from "../Common/authorizedData";
import { deleteHandler } from "./common functions/deleteHandler";
import { fetchDataFromApi } from "./common functions/fetchData";
import GuidesTable from "./common functions/GuidesHandlers/GuidesTable";
import GuideEvents from "./common functions/GuidesHandlers/GuidesEvents";
import AddGuide from "./common functions/GuidesHandlers/AddGuide";
import GuideUsers from "./common functions/GuidesHandlers/GuideUsers";

const AdminGuides = () => {
  const [Guides, setGuides] = useState([]);
  const [editingGuide, setEditingGuide] = useState({
    email: "",
    password: "",
    name: "",
    dob: "",
    gender: "",
    profession: "",
  });
  const [newGuideEmail, setNewGuideEmail] = useState("");
  const [newGuideProfession, setNewGuideProfession] = useState("");
  const [showAddGuideForm, setShowAddGuideForm] = useState(false);
  const [users, setUsers] = useState([]);
  const [showUsers, setShowUsers] = useState(false); // Toggle state for users
  const [errorMessage, setErrorMessage] = useState(""); // Error message state
  const [eventEmail, setEventEmail] = useState("");
  const [eventId, setEventId] = useState(""); // Input for Event ID
  const [events, setEvents] = useState([]);
  const [showEvents, setShowEvents] = useState(false); // Toggle state for events
  const [expandedGuides, setExpandedGuides] = useState({}); // { email: [events] }

  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Guide/GetAllGuides",
      setGuides
    );
    fetchDataFromApi(
      "https://localhost:7095/api/User/GetAllNonGuides",
      setUsers
    );
  }, [Guides]);

  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Event/GetAllEvents",
      setEvents
    );
  }, [events]);

  const handleDelete = async (email) => {
    deleteHandler("Guide", "email", email, setGuides);
  };

  const handleEdit = (Guide) => {
    setEditingGuide({
      email: Guide.email || "",
      password: Guide.password || "",
      name: Guide.name || "",
      dob: Guide.dob || "",
      gender: Guide.gender || "",
      profession: Guide.profession || "",
    });
  };

  const handleSave = async (editedGuide) => {
    if (!editedGuide || !editedGuide.email) return; // Guard clause
    try {
      await apiClient.put(
        `https://localhost:7095/api/Guide/UpdateGuide?email=${editedGuide.email}`,
        editedGuide
      );
      setGuides(
        Guides.map((Guide) =>
          Guide.email === editedGuide.email ? editedGuide : Guide
        )
      );
      setEditingGuide({
        email: "",
        password: "",
        name: "",
        dob: "",
        gender: "",
        profession: "",
      });
    } catch (error) {
      console.error("Error updating Guide:", error);
    }
  };

  const handleAddGuide = async () => {
    if (!newGuideEmail || !newGuideProfession) {
      alert("Please enter both email and profession for the new Guide.");
      return;
    }

    try {
      const response = await apiClient.post(
        `https://localhost:7095/api/Guide/CreateGuide?email=${encodeURIComponent(
          newGuideEmail
        )}`,
        {
          Profession: newGuideProfession,
        }
      );

      if (!response.ok) {
        throw new Error(`Failed to create Guide. Status: ${response.status}`);
      }

      const newGuide = await response.json(); // Assuming JSON response
      if (!newGuide || !newGuide.email) {
        throw new Error("Invalid Guide data received from server.");
      }

      setGuides([...Guides, newGuide]);
      setNewGuideEmail("");
      setNewGuideProfession("");
      setShowAddGuideForm(false); // Hide the form after successful addition
    } catch (error) {
      setErrorMessage();
      // "Failed to create Guide. Please check your credentials."
      // setTimeout(() => setErrorMessage(""), 4000); // Clear the error message after 4 seconds
      // console.error("Error adding Guide:", error);
    }
  };

  const toggleAddGuideForm = () => {
    setShowAddGuideForm(!showAddGuideForm);
  };

  const toggleShowUsers = () => {
    setShowUsers(!showUsers); // Toggle visibility of users' table
  };

  const handleAddEventToGuide = async () => {
    if (!eventEmail || !eventId) {
      alert("Please provide both email and event ID.");
      return;
    }

    try {
      const response = await apiClient.put(
        `https://localhost:7095/api/Guide/AddEvent?id=${encodeURIComponent(
          eventId
        )}&email=${encodeURIComponent(eventEmail)}`
      );

      if (!response.ok) {
        throw new Error(`Failed to add event. Status: ${response.status}`);
      }

      alert("Event added successfully.");
      setEventEmail(""); // Clear the input fields
      setEventId("");
    } catch (error) {
      // setErrorMessage("Failed to add event. Please check your inputs.");
      // setTimeout(() => setErrorMessage(""), 4000); // Clear the error message after 4 seconds
      // console.error("Error adding event:", error);
    } finally {
      setEventEmail(""); // Clear the input fields
      setEventId("");
    }
  };

  const toggleShowEvents = () => {
    setShowEvents(!showEvents);
  };

  const toggleShowEventsForGuide = async (email) => {
    if (expandedGuides[email]) {
      // If already expanded, collapse it
      setExpandedGuides((prev) => {
        const updated = { ...prev };
        delete updated[email];
        return updated;
      });
    } else {
      // Fetch events for the Guide
      try {
        const response = await apiClient.get(
          `https://localhost:7095/api/Guide/GetEvents?email=${encodeURIComponent(
            email
          )}`
        );
        const data = response.data;
        setExpandedGuides((prev) => ({
          ...prev,
          [email]: data || [],
        }));
      } catch (error) {
        console.error(`Error fetching events for guide ${email}:`, error);
        alert("Failed to fetch events for this guide.");
      }
    }
  };

  return (
    <>
      <GuidesTable
        errorMessage={errorMessage}
        Guides={Guides}
        editingGuide={editingGuide}
        setEditingGuide={setEditingGuide}
        handleSave={handleSave}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
        toggleShowEventsForGuide={toggleShowEventsForGuide}
        expandedGuides={expandedGuides}
      />

      <AddGuide
        toggleAddGuideForm={toggleAddGuideForm}
        showAddGuideForm={showAddGuideForm}
        newGuideEmail={newGuideEmail}
        setNewGuideEmail={setNewGuideEmail}
        handleAddGuide={handleAddGuide}
        newGuideProfession={newGuideProfession}
        setNewGuideProfession={setNewGuideProfession}
      />

      <GuideUsers
        toggleShowUsers={toggleShowUsers}
        showUsers={showUsers}
        users={users}
      />
      <br />
      <GuideEvents
        setEventEmail={setEventEmail}
        setEventId={setEventId}
        handleAddEventToGuide={handleAddEventToGuide}
        toggleShowEvents={toggleShowEvents}
        events={events}
        eventEmail={eventEmail}
        eventId={eventId}
        showEvents={showEvents}
      />
    </>
  );
};

export default AdminGuides;
