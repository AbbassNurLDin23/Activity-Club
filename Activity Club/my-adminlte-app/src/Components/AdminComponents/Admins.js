import React, { useEffect, useState } from "react";
import apiClient from "../Common/authorizedData";
import Header from "../Common/PHeader";
import Footer from "../Common/Footer";
import { deleteHandler } from "./common functions/deleteHandler";
import { fetchDataFromApi } from "./common functions/fetchData";
import TableColumn from "./common functions/TableColumn";
import { useUser } from "../../UserContext";

const Admins = () => {
  const [admins, setAdmins] = useState([]);
  const [newAdminEmail, setNewAdminEmail] = useState("");
  const [nonAdmins, setNonAdmins] = useState([]);
  const [showNonAdmins, setShowNonAdmins] = useState(false);
  const { formatDOB } = useUser();
  const [editingAdmin, setEditingAdmin] = useState({
    email: "",
    password: "",
    name: "",
    gender: "",
    dob: "",
  });

  useEffect(() => {
    fetchDataFromApi("https://localhost:7095/api/User/GetAllAdmins", setAdmins);
  }, [admins]);

  const fetchNonAdmins = async () => {
    fetchDataFromApi(
      "https://localhost:7095/api/User/GetAllNonAdmins",
      setNonAdmins
    );
  };

  const handleDelete = async (email) => {
    deleteHandler("Admin", "email", email, setAdmins);
  };

  const handleEdit = (admin) => {
    setEditingAdmin({
      email: admin.email,
      password: admin.password,
      name: admin.name,
      gender: admin.gender,
      dob: admin.dob,
    });
  };

  const handleSave = async (editedAdmin) => {
    try {
      await apiClient.put(
        `https://localhost:7095/api/User/UpdateUser?email=${editedAdmin.email}`,
        editedAdmin
      );
      setAdmins(
        admins.map((admin) =>
          admin.email === editedAdmin.email ? editedAdmin : admin
        )
      );
      setEditingAdmin({
        email: "",
        password: "",
        name: "",
        gender: "",
        dob: "",
      });
    } catch (error) {
      console.error("Error updating admin:", error);
    }
  };

  const handleAddAdmin = async () => {
    if (!newAdminEmail) {
      alert("Please enter an email address for the new admin.");
      return;
    }

    try {
      await apiClient.put(
        `https://localhost:7095/api/User/AddAdmins?email=${newAdminEmail}`,
        {}
      );
      setAdmins([...admins, { email: newAdminEmail }]);
      setNewAdminEmail("");
    } catch (error) {
      console.error("Error adding admin:", error);
    }
  };

  const toggleShowNonAdmins = () => {
    setShowNonAdmins(!showNonAdmins);
    if (!showNonAdmins) {
      fetchNonAdmins();
    }
  };

  return (
    <>
      <Header />
      <div className="container">
        <h2>Admins</h2>
        <table>
          <thead>
            <tr>
              <th>Email</th>
              <th>Password</th>
              <th>Name</th>
              <th>DOB</th>
              <th>Gender</th>
              {/* <th>Roles</th> */}
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {admins.map((admin) => (
              <tr key={admin.email}>
                <TableColumn
                  isEditing={editingAdmin.email}
                  rowKey={admin.email}
                  type="text"
                  editedValue={editingAdmin.email}
                  setEdited={setEditingAdmin}
                  edited={editingAdmin}
                  fieldName="email"
                  fieldValue={admin.email}
                />
                <TableColumn
                  isEditing={editingAdmin.email}
                  rowKey={admin.email}
                  type="text"
                  editedValue={editingAdmin.password}
                  setEdited={setEditingAdmin}
                  edited={editingAdmin}
                  fieldName="password"
                  fieldValue={admin.password}
                />
                <TableColumn
                  isEditing={editingAdmin.email}
                  rowKey={admin.email}
                  type="text"
                  editedValue={editingAdmin.name}
                  setEdited={setEditingAdmin}
                  edited={editingAdmin}
                  fieldName="name"
                  fieldValue={admin.name}
                />
                <TableColumn
                  isEditing={editingAdmin.email}
                  rowKey={admin.email}
                  type="date"
                  editedValue={editingAdmin.dob}
                  setEdited={setEditingAdmin}
                  edited={editingAdmin}
                  fieldName="dob"
                  fieldValue={formatDOB(admin.dob)}
                />

                <td>
                  {editingAdmin?.email === admin.email ? (
                    <select
                      value={editingAdmin.gender}
                      onChange={(e) =>
                        setEditingAdmin({
                          ...editingAdmin,
                          gender: e.target.value,
                        })
                      }
                    >
                      <option value="Male">Male</option>
                      <option value="Female">Female</option>
                      <option value="Other">Other</option>
                    </select>
                  ) : (
                    admin.gender
                  )}
                </td>
                {/* <td>
                  {editingAdmin?.email === admin.email ? (
                    <input
                      type="text"
                      value={editingAdmin.roles?.join(", ") || ""}
                      onChange={(e) =>
                        setEditingAdmin({
                          ...editingAdmin,
                          roles: e.target.value.split(","),
                        })
                      }
                    />
                  ) : (
                    admin.roles?.join(", ") || "N/A"
                  )}
                </td> */}
                <td>
                  {editingAdmin?.email === admin.email ? (
                    <button onClick={() => handleSave(editingAdmin)}>
                      Save
                    </button>
                  ) : (
                    <button onClick={() => handleEdit(admin)}>Edit</button>
                  )}
                  <button onClick={() => handleDelete(admin.email)}>
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
        <div>
          <h3>Add Admin</h3>
          <input
            type="email"
            placeholder="Enter email"
            value={newAdminEmail}
            onChange={(e) => setNewAdminEmail(e.target.value)}
          />
          <button onClick={handleAddAdmin}>Add Admin</button>
        </div>
        <button onClick={toggleShowNonAdmins}>
          {showNonAdmins ? "Hide Non-Admins" : "Show Non-Admins"}
        </button>
        {showNonAdmins && (
          <table>
            <thead>
              <tr>
                <th>Email</th>
                <th>Password</th>
                <th>Name</th>
                <th>DOB</th>
                <th>Gender</th>
                <th>Roles</th>
              </tr>
            </thead>
            <tbody>
              {nonAdmins.map((nonAdmin) => (
                <tr key={nonAdmin.email}>
                  <td>{nonAdmin.email}</td>
                  <td>{nonAdmin.password}</td>
                  <td>{nonAdmin.name}</td>
                  <td>{nonAdmin.dob}</td>
                  <td>{nonAdmin.gender}</td>
                  <td>{nonAdmin.roles?.join(", ") || "N/A"}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
      <Footer />
    </>
  );
};

export default Admins;
