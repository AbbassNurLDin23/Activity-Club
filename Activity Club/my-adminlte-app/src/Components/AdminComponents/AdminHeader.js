import React from "react";
import { NavLink } from "react-router-dom";
import { useUser } from "../../UserContext";

const AdminHeader = () => {
  const { userRole } = useUser();

  return (
    <header className="admin-header">
      <nav className="admin-header-nav">
        <div>
          <h1>Activity Club</h1>
          <button
            type="button"
            data-toggle="collapse"
            data-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span></span>
          </button>
          <div id="navbarNav">
            <ul>
              <li>
                <NavLink
                  to="/admins"
                  className={({ isActive }) => (isActive ? "active-link" : "")}
                >
                  Admins
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/guides"
                  className={({ isActive }) => (isActive ? "active-link" : "")}
                >
                  Guides
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/events"
                  className={({ isActive }) => (isActive ? "active-link" : "")}
                >
                  Events
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/members"
                  className={({ isActive }) => (isActive ? "active-link" : "")}
                >
                  Members
                </NavLink>
              </li>
              <li>
                <NavLink
                  to="/lookups"
                  className={({ isActive }) => (isActive ? "active-link" : "")}
                >
                  Lookups
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
      <div className="admin-header-profile">
        <NavLink to="/profile">
          <i className="fas fa-user"></i>{" "}
          {/* Update with the appropriate icon */}
        </NavLink>
      </div>
    </header>
  );
};

export default AdminHeader;
