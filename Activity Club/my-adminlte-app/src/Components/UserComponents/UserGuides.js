import axios from "axios";
import React, { useState, useEffect } from "react";

const UserGuides = () => {
  const [guides, setGuides] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(
          "https://localhost:7095/api/Guide/GetAllGuides"
        );
        console.log("API Response:", response.data); // Log API response
        setGuides(response.data);
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };

    fetchData();
  }, []);

  return (
    <div className="container">
      <h2>Guides</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Profession</th>
            <th>Email</th>
            <th>Password</th>
            <th>Name</th>
            <th>DOB</th>
            <th>Gender</th>
            <th>Roles</th>
          </tr>
        </thead>
        <tbody>
          {guides.map((guide, index) => (
            <tr key={guide.email ? guide.email.trim() : index}>
              <td>{guide.profession || "N/A"}</td>
              <td>{guide.email || "N/A"}</td>
              <td>{guide.password || "N/A"}</td>
              <td>{guide.name || "N/A"}</td>
              <td>
                {guide.dob ? new Date(guide.dob).toLocaleDateString() : "N/A"}
              </td>
              <td>{guide.gender || "N/A"}</td>
              <td>{guide.roles ? guide.roles.join(", ") : "N/A"}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default UserGuides;
