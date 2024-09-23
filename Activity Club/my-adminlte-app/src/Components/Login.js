import React, { useState } from "react";
import axios from "axios";
import Footer from "./Common/Footer";
import LoginHeader from "./Common/LoginHeader";
import Form from "./Common/Form"; // Adjust path as necessary
import { useUser } from "../UserContext"; // Import useUser hook
import { useNavigate } from "react-router-dom"; // Import useNavigate

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [success, setSuccess] = useState("");
  const { setUserRole, setUserEmail, setUserType } = useUser(); // Get setUserEmail from context
  const navigate = useNavigate(); // Initialize useNavigate

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      // Attempt login with AdminLogin endpoint (for admins)
      let response = await axios.post(
        "https://localhost:7095/api/Authorization/AdminLogin",
        { email, password },
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      );

      // Admin login successful - store token, set user role and email
      const token = response.data; // Adjust according to the actual response structure
      localStorage.setItem("token", token);
      setUserRole("admin"); // Set user role as admin
      setUserEmail(email); // Set global email
      setSuccess("Admin login successful and authorized!");
      setError("");
      console.log("Token:", token);

      // Redirect to /home after admin login
      navigate("/admins");
    } catch (adminError) {
      // If AdminLogin fails, try the regular Login endpoint
      try {
        let response = await axios.post(
          "https://localhost:7095/api/Authorization/Login",
          { email, password },
          {
            headers: {
              "Content-Type": "application/json",
            },
          }
        );
        let type = await axios.get(
          `https://localhost:7095/api/User/GetUserType?email=${email}`
        );

        setUserType(type.data);
        // Regular user login successful - set user role and email
        setSuccess("Login successful!");
        setError("");
        setUserRole("user"); // Set user role as user
        setUserEmail(email); // Set global email
        console.log("Regular login success");

        // Redirect to /home after user login
        navigate("/home");
      } catch (loginError) {
        // Both AdminLogin and Login failed, show error message
        setError("Login failed. Please check your credentials.");
        setSuccess("");
        console.error("Error:", loginError);
      }
    }
  };

  const inputs = [
    {
      type: "email",
      value: email,
      onChange: (e) => setEmail(e.target.value),
      placeholder: "Email",
      name: "email",
      icon: "fas fa-envelope", // FontAwesome email icon
    },
    {
      type: "password",
      value: password,
      onChange: (e) => setPassword(e.target.value),
      placeholder: "Password",
      name: "password",
      icon: "fas fa-lock", // FontAwesome lock icon for password
    },
  ];

  return (
    <div className="App">
      <LoginHeader />
      <header className="App-header">
        <h1>Login</h1>

        {/* Bootstrap container to control the width */}
        <div className="container">
          <div className="row justify-content-center">
            <div className="col-md-6 col-sm-8">
              {/* Display success message */}
              {success && (
                <div className="alert alert-success" role="alert">
                  {success}
                </div>
              )}

              {/* Display error message */}
              {error && (
                <div className="alert alert-danger" role="alert">
                  {error}
                </div>
              )}

              {/* Use Form component to render form */}
              <Form
                onSubmit={handleLogin}
                inputs={inputs}
                buttonText="Submit"
                action="./Home"
              />

              {/* Signup link */}
              <div className="input-group mb-3">
                <a href="/Signup" className="small underline-link">
                  Create New User
                </a>
              </div>
            </div>
          </div>
        </div>
      </header>
      <Footer />
    </div>
  );
}

export default Login;
