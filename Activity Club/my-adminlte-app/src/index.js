import React from "react";
import ReactDOM from "react-dom/client";
import "./design/index.css";
import "./design/App.css";
import Login from "./Components/Login";
import reportWebVitals from "./reportWebVitals";
import { UserProvider } from "./UserContext";
import "animate.css";
import "admin-lte/dist/css/adminlte.min.css";
import "@fortawesome/fontawesome-free/css/all.min.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap/dist/css/bootstrap.min.css"; // Bootstrap first
import "admin-lte/dist/css/adminlte.min.css"; // AdminLTE next
import "jquery/dist/jquery.min.js";
import "popper.js/dist/umd/popper.min.js";
import "bootstrap/dist/js/bootstrap.min.js";
import "admin-lte/dist/js/adminlte.min.js";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom"; // Import BrowserRouter, Routes, and Route
import Home from "./Components/Home";
import Profile from "./Components/Profile";
import SignupPage from "./Components/SignupPage";
import Events from "./Components/Events";
import Register from "./Components/Register";
import Guides from "./Components/Guides";
import Admins from "./Components/AdminComponents/Admins";
import AdminLookups from "./Components/AdminComponents/AdminLookups";
import AdminMembers from "./Components/AdminComponents/AdminMembers";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  <React.StrictMode>
    <UserProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Login />} />
          <Route path="/Signup" element={<SignupPage />} />
          <Route path="/home" element={<Home />} />
          <Route path="/profile" element={<Profile />} />
          <Route path="/events" element={<Events />} />
          <Route path="/register" element={<Register />} />
          <Route path="/guides" element={<Guides />} />
          <Route path="/members" element={<AdminMembers />} />
          <Route path="/admins" element={<Admins />} />
          <Route path="/lookups" element={<AdminLookups />} />
        </Routes>
      </Router>
    </UserProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
