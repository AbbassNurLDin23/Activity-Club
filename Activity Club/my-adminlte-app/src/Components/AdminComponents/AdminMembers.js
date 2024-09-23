import React, { useEffect, useState } from "react";
import apiClient from "../Common/authorizedData";
import Header from "../Common/PHeader";
import Footer from "../Common/Footer";
import { deleteHandler } from "./common functions/deleteHandler";
import { fetchDataFromApi } from "./common functions/fetchData";
import MemberTable from "./common functions/MembersHandlers/MemberTable";
import AddMember from "./common functions/MembersHandlers/AddMember";
import MemberUsers from "./common functions/MembersHandlers/MemberUsers";
import AddEvent from "./common functions/MembersHandlers/MemberAddEvent";

const AdminMembers = () => {
  const [Members, setMembers] = useState([]);
  const [newMemberEmail, setNewMemberEmail] = useState("");
  const [newMemberProfession, setNewMemberProfession] = useState("");
  const [newMemberMN, setNewMemberMN] = useState("");
  const [newMemberEN, setNewMemberEN] = useState("");
  const [newMemberNationality, setNewMemberNationality] = useState("");
  const [showAddMemberForm, setShowAddMemberForm] = useState(false);
  const [users, setUsers] = useState([]);
  const [showUsers, setShowUsers] = useState(false); // Toggle state for users
  const [errorMessage, setErrorMessage] = useState(""); // Error message state
  const [eventEmail, setEventEmail] = useState("");
  const [eventId, setEventId] = useState(""); // Input for Event ID
  const [events, setEvents] = useState([]);
  const [showEvents, setShowEvents] = useState(false); // Toggle state for events
  const [editingMember, setEditingMember] = useState({
    email: "",
    password: "",
    name: "",
    dob: "",
    gender: "",
    profession: "",
    mobileNumber: "",
    emergencyNumber: "",
    nationality: "",
  });

  // New state to manage expanded members and their events
  const [expandedMembers, setExpandedMembers] = useState({}); // { email: [events] }

  // Fetch Members and Users on component mount
  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Member/GetAllMembers",
      setMembers
    );

    fetchDataFromApi(
      "https://localhost:7095/api/User/GetAllNonMembers",
      setUsers
    );
  }, [events, Members, expandedMembers]); // Removed Members from dependency array to prevent infinite loop

  // Fetch Events on component mount
  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Event/GetAllEvents",
      setEvents
    );
  }, []); // Removed events from dependency array to prevent infinite loop

  const formatDOB = (dob) => {
    const dateObject = new Date(dob);
    return isNaN(dateObject.getTime())
      ? "Not provided"
      : dateObject.toLocaleDateString();
  };

  const handleDelete = async (email) => {
    deleteHandler("Member", "email", email, setMembers);
  };

  const handleEdit = (Member) => {
    setEditingMember({
      email: Member.email,
      password: Member.password,
      name: Member.name,
      dob: Member.dob,
      gender: Member.gender,
      profession: Member.profession,
      mobileNumber: Member.mobileNumber,
      emergencyNumber: Member.emergencyNumber,
      nationality: Member.nationality,
    });
  };

  const handleSave = async (editedMember) => {
    if (!editedMember || !editedMember.email) return; // Guard clause
    try {
      await apiClient.put(
        `https://localhost:7095/api/Member/UpdateMember?email=${editedMember.email}`,
        editedMember
      );
      setMembers(
        Members.map((Member) =>
          Member.email === editedMember.email ? editedMember : Member
        )
      );
      setEditingMember({
        email: "",
        password: "",
        name: "",
        dob: "",
        gender: "",
        profession: "",
        mobileNumber: "",
        emergencyNumber: "",
        nationality: "",
      });
    } catch (error) {
      console.error("Error updating Member:", error);
    }
  };

  const handleAddMember = async () => {
    if (!newMemberEmail || !newMemberProfession) {
      alert("Please enter both email and profession for the new Member.");
      return;
    }

    try {
      const response = await apiClient.post(
        `https://localhost:7095/api/Member/CreateMember?email=${encodeURIComponent(
          newMemberEmail
        )}`,
        {
          Profession: newMemberProfession,
          MobileNumber: newMemberMN,
          EmergencyNumber: newMemberEN,
          Nationality: newMemberNationality,
        }
      );

      // Assuming response.data contains the new member
      const newMember = response.data;
      // if (!newMember || !newMember.email) {
      //   throw new Error("Invalid Member data received from server.");
      // }

      setMembers([...Members, newMember]);
      setNewMemberEmail("");
      setNewMemberProfession("");
      setNewMemberMN("");
      setNewMemberEN("");
      setNewMemberNationality("");
      setShowAddMemberForm(false); // Hide the form after successful addition
    } catch (error) {
      // setErrorMessage(
      //   "Failed to create Member. Please check your credentials."
      // );
      // setTimeout(() => setErrorMessage(""), 4000); // Clear the error message after 4 seconds
      // console.error("Error adding Member:", error);
    }
  };

  const toggleAddMemberForm = () => {
    setShowAddMemberForm(!showAddMemberForm);
  };

  const toggleShowUsers = () => {
    setShowUsers(!showUsers); // Toggle visibility of users' table
  };

  const handleAddEventToMember = async () => {
    if (!eventEmail || !eventId) {
      alert("Please provide both email and event ID.");
      return;
    }

    try {
      const response = await apiClient.put(
        `https://localhost:7095/api/Member/AddEvent?id=${encodeURIComponent(
          eventId
        )}&email=${encodeURIComponent(eventEmail)}`
      );

      // Assuming a successful response indicates the event was added
      alert("Event added successfully.");
      setEventEmail(""); // Clear the input fields
      setEventId("");
    } catch (error) {
      setErrorMessage("Failed to add event. Please check your inputs.");
      setTimeout(() => setErrorMessage(""), 4000); // Clear the error message after 4 seconds
      console.error("Error adding event:", error);
    }
  };

  // Function to toggle showing events for a specific member
  const toggleShowEventsForMember = async (email) => {
    if (expandedMembers[email]) {
      // If already expanded, collapse it
      setExpandedMembers((prev) => {
        const updated = { ...prev };
        delete updated[email];
        return updated;
      });
    } else {
      // Fetch events for the member
      try {
        const response = await apiClient.get(
          `https://localhost:7095/api/Member/GetEvents?email=${encodeURIComponent(
            email
          )}`
        );
        const data = response.data;
        setExpandedMembers((prev) => ({
          ...prev,
          [email]: data || [],
        }));
      } catch (error) {
        console.error(`Error fetching events for member ${email}:`, error);
        alert("Failed to fetch events for this member.");
      }
    }
  };

  const toggleShowEvents = () => {
    setShowEvents(!showEvents); // Toggle visibility of the events section
  };

  return (
    <>
      <Header />
      <MemberTable
        errorMessage={errorMessage}
        Members={Members}
        editingMember={editingMember}
        setEditingMember={setEditingMember}
        handleSave={handleSave}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
        toggleShowEventsForMember={toggleShowEventsForMember}
        expandedMembers={expandedMembers}
      />
      <AddMember
        toggleAddMemberForm={toggleAddMemberForm}
        showAddMemberForm={showAddMemberForm}
        newMemberEmail={newMemberEmail}
        setNewMemberEmail={setNewMemberEmail}
        newMemberProfession={newMemberProfession}
        setNewMemberProfession={setNewMemberProfession}
        newMemberMN={newMemberMN}
        setNewMemberMN={setNewMemberMN}
        newMemberEN={newMemberEN}
        setNewMemberEN={setNewMemberEN}
        newMemberNationality={newMemberNationality}
        setNewMemberNationality={setNewMemberNationality}
        handleAddMember={handleAddMember}
      />

      <MemberUsers
        toggleShowUsers={toggleShowUsers}
        showUsers={showUsers}
        users={users}
      />

      <AddEvent
        eventEmail={eventEmail}
        setEventEmail={setEventEmail}
        eventId={eventId}
        setEventId={setEventId}
        handleAddEventToMember={handleAddEventToMember}
        toggleShowEvents={toggleShowEvents}
        showEvents={showEvents}
        events={events}
      />
      <Footer />
    </>
  );
};

export default AdminMembers;
