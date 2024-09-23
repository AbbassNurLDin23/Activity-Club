import React, { useState } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useUser } from "../UserContext"; // Import useUser hook
import Footer from "./Common/Footer";
import LoginHeader from "./Common/LoginHeader";

const Signup = () => {
  const [formData, setFormData] = useState({
    email: "",
    password: "",
    name: "",
    dob: "",
    gender: "",
  });

  const [error, setError] = useState(""); // To store error messages

  const navigate = useNavigate(); // Hook for navigation
  const { setUserRole, setUserEmail, setUserType } = useUser(); // Use context to set userRole and userEmail

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(""); // Clear previous error before submitting

    try {
      const response = await axios.post(
        "https://localhost:7095/api/User/CreateUser",
        formData
      );
      console.log("User created successfully:", response.data);
      await axios.put(
        `https://localhost:7095/api/User/AddUserRole?email=${formData.email}&role=user`
      );

      // Set userRole to 'user' and userEmail to the email provided by the user
      setUserRole("user");
      setUserEmail(formData.email); // Set userEmail to the email provided by the user
      let type = await axios.get(
        `https://localhost:7095/api/User/GetUserType?email=${formData.email}`
      );

      setUserType(type.data);
      // Redirect to the /home page after successful submission
      navigate("/home");
    } catch (error) {
      if (error.response && error.response.status === 400) {
        // If the server returns a 400 Bad Request, set a meaningful error message
        setError("Incorrect or invalid credentials. Please try again.");
      } else {
        // For other types of errors
        setError("An unexpected error occurred. Please try again.");
      }
      console.error("There was an error creating the user:", error);
    }
  };

  return (
    <>
      <LoginHeader />
      <form onSubmit={handleSubmit} className="signup-form">
        <label>
          Email:
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Password:
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Name:
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Date of Birth:
          <input
            type="date"
            name="dob"
            value={formData.dob}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Gender:
          <select name="gender" value={formData.gender} onChange={handleChange}>
            <option value="">Select Gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
            <option value="Other">Other</option>
          </select>
        </label>
        <button type="submit">Sign Up</button>

        {/* Display error message if there's any */}
        {error && <p style={{ color: "red" }}>{error}</p>}
      </form>
      <Footer />
    </>
  );
};

export default Signup;
