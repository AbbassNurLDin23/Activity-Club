import React, { useEffect, useState } from "react";
import axios from "axios";
import { useUser } from "../../UserContext";
import { useNavigate } from "react-router-dom";

const UserEvents = () => {
  const [events, setEvents] = useState([]);
  const [error, setError] = useState("");
  const { userEmail } = useUser();
  const [message, setMessage] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7095/api/Event/GetAllEvents"
        );
        setEvents(response.data);
      } catch (err) {
        console.error("Error fetching events:", err);
        setError("Error fetching events.");
      }
    };

    fetchEvents();
  }, []);

  const registerForEvent = async (eventId) => {
    try {
      const joinedMember = await axios.get(
        `https://localhost:7095/api/Event/HasMember?email=${userEmail}&id=${eventId}`
      );
      const joinedGuide = await axios.get(
        `https://localhost:7095/api/Event/HasGuide?email=${userEmail}&id=${eventId}`
      );
      if (joinedMember.data === false && joinedGuide.data === false) {
        const isMember = await axios.get(
          `https://localhost:7095/api/User/GetUserType?email=${userEmail}`
        );
        if (isMember.data === "member") {
          const addMemberResponse = await axios.put(
            `https://localhost:7095/api/Event/AddMember?email=${userEmail}&id=${eventId}`
          );
          const addEventResponse = await axios.put(
            `https://localhost:7095/api/Member/AddEvent?id=${eventId}&email=${userEmail}`
          );

          setMessage("Event Added Successfully.");
        } else {
          navigate("/register", { state: { eventId } });
        }
      } else {
        setMessage("You are already registered for this event.");
      }
    } catch (err) {
      console.error("Error registering for event:", err);
      setError("Error registering for the event.");
    }
  };

  return (
    <div className="container">
      <h1>Upcoming Events</h1>
      {error && <div className="alert alert-danger">{error}</div>}
      {message && <div className="alert alert-warning">{message}</div>}
      <table className="table table-bordered">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Description</th>
            <th>Cost</th>
            <th>Status</th>
            <th>Category</th>
            <th>Destination</th>
            <th>Date From</th>
            <th>Date To</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {events.map((event) => (
            <tr key={event.id}>
              <td>{event.id}</td>
              <td>{event.name}</td>
              <td>{event.description}</td>
              <td>{event.cost}</td>
              <td>{event.status}</td>
              <td>{event.category}</td>
              <td>{event.destination}</td>
              <td>{new Date(event.dateFrom).toLocaleDateString()}</td>
              <td>{new Date(event.dateTo).toLocaleDateString()}</td>
              <td>
                <button
                  className="btn btn-primary"
                  onClick={() => registerForEvent(event.id)}
                >
                  Register
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserEvents;
