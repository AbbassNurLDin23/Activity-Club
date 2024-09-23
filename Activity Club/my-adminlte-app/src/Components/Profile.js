import React, { useEffect, useState } from "react";
import Header from "./Common/PHeader";
import Footer from "./Common/Footer";
import { useUser } from "../UserContext";
import axios from "axios";

const Profile = () => {
  const { userEmail, userType, addedEvent, setAddedEvent } = useUser();
  const [userData, setUserData] = useState(null);
  const [error, setError] = useState("");
  const [isEditing, setIsEditing] = useState(false);
  const [editData, setEditData] = useState({});
  const [showConfirmation, setShowConfirmation] = useState(false);
  const [userEvents, setUserEvents] = useState([]);

  useEffect(() => {
    const fetchUserData = async () => {
      try {
        const response = await axios.get(
          `https://localhost:7095/api/User/GetUserByEmail`,
          {
            params: { email: userEmail },
          }
        );
        setUserData(response.data);
        setEditData(response.data); // Initialize editData with fetched data
      } catch (err) {
        console.error("Error fetching user data:", err);
        setError("Error fetching user data.");
      }
    };
    fetchUserData();
  }, [userEmail]);

  useEffect(() => {
    const fetchEvents = async (email) => {
      try {
        let res;
        if (userType === "guide") {
          res = await axios.get(
            `https://localhost:7095/api/Guide/GetGuideByEmail`,
            {
              params: { email: email },
            }
          );
          setUserEvents(res.data.guideEvents);
        } else if (userType === "member") {
          res = await axios.get(
            `https://localhost:7095/api/Member/GetMemberByEmail`,
            {
              params: { email: email },
            }
          );
          setUserEvents(res.data.memberEvents);
        }
      } catch (err) {
        console.error("Error fetching events:", err);
        setError("Error fetching events.");
      }
    };

    fetchEvents(userEmail); // fetch events again on email change or addedEvent state change
  }, [userEmail, userType]); // removed userEvents from dependencies

  const handleEdit = () => {
    setIsEditing(true);
    setEditData({ ...userData, password: "" }); // Clear the password field when entering edit mode
  };

  const handleSave = () => {
    setShowConfirmation(true); // Show confirmation after clicking Save
  };

  const handleConfirmSave = async () => {
    try {
      const dataToSend = { ...editData };

      // If date of birth is set, format it
      if (dataToSend.dob) {
        dataToSend.dob = new Date(dataToSend.dob).toISOString().split("T")[0];
      } else {
        dataToSend.dob = null;
      }

      // If password is empty, remove it from the payload
      if (!dataToSend.password) {
        delete dataToSend.password;
      }

      console.log("Data to be sent:", dataToSend);

      // Make the PUT request with email as query parameter and data as JSON body
      const response = await axios.put(
        `https://localhost:7095/api/User/UpdateUser`, // API endpoint
        dataToSend, // JSON body
        {
          params: { email: userEmail }, // Query parameter for email
        }
      );

      // Update user data with the response
      setUserData(dataToSend);
      console.log("Response data:", response.data);

      // Close editing and confirmation dialogs
      setIsEditing(false);
      setShowConfirmation(false);
    } catch (err) {
      // Log validation errors, if any
      if (err.response?.status === 400 && err.response?.data?.errors) {
        console.error("Validation Errors:", err.response.data.errors);
      } else {
        console.error(
          "Error updating user data:",
          err.response?.data || err.message
        );
      }
      setError("Error updating user data.");
    }
  };

  const handleCancel = () => {
    setEditData(userData); // Revert to original data
    setIsEditing(false);
    setShowConfirmation(false);
  };

  const handleChange = (e) => {
    setEditData({ ...editData, [e.target.name]: e.target.value });
  };

  return (
    <>
      <Header />
      <div className="container mt-5 profile-container">
        <h1 className="text-center mb-5">Your Profile</h1>

        {error && (
          <div className="alert alert-danger" role="alert">
            {error}
          </div>
        )}

        {showConfirmation && (
          <div className="confirmation-box text-center mb-3">
            <p>Are you sure you want to save the changes?</p>
            <button className="btn btn-secondary" onClick={handleCancel}>
              Cancel
            </button>
            <button
              className="btn btn-primary ml-2"
              onClick={handleConfirmSave}
            >
              OK
            </button>
          </div>
        )}

        {userData ? (
          isEditing ? (
            <div className="edit-form">
              <div className="form-group">
                <label>Email:</label>
                <input
                  type="text"
                  name="email"
                  value={editData.email || ""} // Ensure default value
                  disabled
                  className="form-control"
                />
              </div>
              <div className="form-group">
                <label>Password:</label>
                <input
                  type="password"
                  name="password"
                  value={editData.password || ""}
                  onChange={handleChange}
                  className="form-control"
                  placeholder="Enter new password"
                />
              </div>
              <div className="form-group">
                <label>Name:</label>
                <input
                  type="text"
                  name="name"
                  value={editData.name || ""} // Ensure default value
                  onChange={handleChange}
                  className="form-control"
                />
              </div>
              <input
                type="date"
                name="dob"
                value={
                  editData.dob
                    ? new Date(editData.dob).toISOString().split("T")[0]
                    : ""
                } // Format date to "yyyy-MM-dd"
                onChange={handleChange}
                className="form-control"
              />

              <div className="form-group">
                <label>Gender:</label>
                <input
                  type="text"
                  name="gender"
                  value={editData.gender || ""} // Ensure default value
                  onChange={handleChange}
                  className="form-control"
                />
              </div>

              <button className="btn btn-save" onClick={handleSave}>
                Save
              </button>
              <button className="btn btn-save ml-2" onClick={handleCancel}>
                Cancel
              </button>
            </div>
          ) : (
            <div className="profile-details">
              <table className="table">
                <tbody>
                  <tr>
                    <th>Email</th>
                    <td>{userData.email}</td>
                  </tr>
                  <tr>
                    <th>Name</th>
                    <td>{userData.name}</td>
                  </tr>
                  <tr>
                    <th>Date of Birth</th>
                    <td>
                      {userData.dob
                        ? new Date(userData.dob).toLocaleDateString()
                        : "N/A"}
                    </td>
                  </tr>
                  <tr>
                    <th>Gender</th>
                    <td>{userData.gender || "N/A"}</td>
                  </tr>
                  <tr>
                    <th>Roles</th>
                    <td>
                      {userData.roles ? userData.roles.join(", ") : "N/A"}
                    </td>
                  </tr>
                </tbody>
              </table>
              <button className="btn btn-save" onClick={handleEdit}>
                Edit
              </button>
              <table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Cost</th>
                    <th>Status</th>
                    <th>Destination</th>
                    <th>Category</th>
                    <th>Date From</th>
                    <th>Date To</th>
                  </tr>
                </thead>
                <tbody>
                  {userEvents && userEvents.length > 0 ? (
                    userEvents.map((event) => (
                      <tr key={event.id}>
                        <td>{event.name}</td>
                        <td>{event.description}</td>
                        <td>{event.cost}</td>
                        <td>{event.status}</td>
                        <td>{event.destination}</td>
                        <td>{event.category}</td>
                        <td>{new Date(event.dateFrom).toLocaleDateString()}</td>
                        <td>{new Date(event.dateTo).toLocaleDateString()}</td>
                      </tr>
                    ))
                  ) : (
                    <tr>
                      <td colSpan="8">No events found.</td>
                    </tr>
                  )}
                </tbody>
              </table>
            </div>
          )
        ) : (
          !error && <p className="text-center">Loading profile...</p>
        )}
      </div>
      <Footer />
    </>
  );
};

export default Profile;
