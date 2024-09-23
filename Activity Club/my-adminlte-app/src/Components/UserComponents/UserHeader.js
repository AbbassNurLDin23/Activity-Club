import React from "react";
import { NavLink } from "react-router-dom";

const UserHeader = () => {
  return (
    <header className="user-header">
      <nav className="navbar navbar-expand-lg navbar-dark">
        <div className="container">
          <h1 className="navbar-brand">Activity Club</h1>
          <button
            type="button"
            className="navbar-toggler"
            data-toggle="collapse"
            data-target="#navbarNav"
            aria-controls="navbarNav"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          <div className="collapse navbar-collapse" id="navbarNav">
            <ul className="navbar-nav ml-auto">
              <li className="nav-item">
                <NavLink
                  className="nav-link"
                  to="/home"
                  activeClassName="active-link"
                >
                  Home
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink
                  className="nav-link"
                  to="/events"
                  activeClassName="active-link"
                >
                  Events
                </NavLink>
              </li>
              <li className="nav-item">
                <NavLink
                  className="nav-link"
                  to="/guides"
                  activeClassName="active-link"
                >
                  Guides
                </NavLink>
              </li>
            </ul>
          </div>
        </div>
      </nav>
      <div className="profile-icon">
        <NavLink to="/profile">
          <i className="fas fa-user-circle"></i>
        </NavLink>
      </div>
    </header>
  );
};

export default UserHeader;
