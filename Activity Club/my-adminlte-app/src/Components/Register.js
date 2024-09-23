import React, { useState, useEffect } from "react";
import axios from "axios";
import { useUser } from "../UserContext";
import { useLocation, useNavigate } from "react-router-dom";

const Register = () => {
  const { userEmail, addedEvent } = useUser();
  const location = useLocation(); // To retrieve the passed event ID
  const navigate = useNavigate();
  const { eventId } = location.state || {}; // Get the eventId from state

  const [formData, setFormData] = useState({
    mobileNumber: "",
    emergencyNumber: "",
    profession: "",
    nationality: "",
  });

  // Handle input changes
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Prepare payload excluding email
    const payload = {
      mobileNumber: formData.mobileNumber,
      emergencyNumber: formData.emergencyNumber || null,
      profession: formData.profession || null,
      nationality: formData.nationality || null,
    };

    // Prepare query parameters
    const encodedEmail = encodeURIComponent(userEmail);
    const encodedEventId = encodeURIComponent(eventId);

    try {
      // Make API request with email as query parameter
      const memberResponse = await axios.post(
        `https://localhost:7095/api/Member/CreateMember?email=${encodedEmail}`,
        payload,
        { headers: { "Content-Type": "application/json" } } // Ensure proper content-type
      );

      // Handle event and member linking
      const addMemberResponse = await axios.put(
        `https://localhost:7095/api/Event/AddMember?email=${encodedEmail}&id=${encodedEventId}`
      );
      const addEventResponse = await axios.put(
        `https://localhost:7095/api/Member/AddEvent?id=${encodedEventId}&email=${encodedEmail}`
      );

      // Confirmation dialog
      const isConfirmed = window.confirm(
        "Registration was successful. Do you want to proceed to the events page?"
      );

      if (isConfirmed) {
        navigate("/events");
      }
    } catch (error) {
      console.error("Error during registration:", error);
      alert("Registration failed. Please check the console for details.");
    }
  };

  return (
    <div className="container">
      <h1>Register</h1>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Mobile Number:</label>
          <input
            type="number"
            name="mobileNumber"
            value={formData.mobileNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div>
          <label>Emergency Number (optional):</label>
          <input
            type="number"
            name="emergencyNumber"
            value={formData.emergencyNumber}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Profession:</label>
          <input
            type="text"
            name="profession"
            value={formData.profession}
            onChange={handleChange}
          />
        </div>
        <div>
          <label>Nationality:</label>
          <input
            type="text"
            name="nationality"
            value={formData.nationality}
            onChange={handleChange}
          />
        </div>
        <button type="submit">Register</button>
      </form>
    </div>
  );
};

export default Register;
