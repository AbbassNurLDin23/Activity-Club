import TableColumn from "../TableColumn";
import JoinedEvents from "./GuideJoinedEvents";
import { useUser } from "../../../../UserContext";
import React from "react";

const GuidesTable = ({
  errorMessage,
  Guides,
  editingGuide,
  setEditingGuide,
  handleSave,
  handleEdit,
  handleDelete,
  toggleShowEventsForGuide,
  expandedGuides,
}) => {
  return (
    <>
      <h2>Guides</h2>
      {errorMessage && (
        <div style={{ color: "red", marginBottom: "10px" }}>{errorMessage}</div>
      )}
      <table>
        <thead>
          <tr>
            <th>Email</th>
            <th>Password</th>
            <th>Name</th>
            <th>Date of Birth</th>
            <th>Gender</th>
            <th>Roles</th>
            <th>Profession</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {Guides.map((Guide) => (
            <React.Fragment key={Guide.email}>
              <tr>
                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="text"
                  editedValue={editingGuide.email}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="email"
                  fieldValue={Guide.email}
                />
                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="text"
                  editedValue={editingGuide.password}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="password"
                  fieldValue={Guide.password}
                />

                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="text"
                  editedValue={editingGuide.name}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="name"
                  fieldValue={Guide.name}
                />
                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="date"
                  editedValue={editingGuide.dob}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="dob"
                  fieldValue={Guide.dob}
                />
                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="text"
                  editedValue={editingGuide.gender}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="gender"
                  fieldValue={Guide.gender}
                />
                <TableColumn
                  isEditing={editingGuide.email}
                  rowKey={Guide.email}
                  type="text"
                  editedValue={editingGuide.profession}
                  setEdited={setEditingGuide}
                  edited={editingGuide}
                  fieldName="profession"
                  fieldValue={Guide.profession}
                />
                <td>
                  {editingGuide?.email === Guide.email ? (
                    <button onClick={() => handleSave(editingGuide)}>
                      Save
                    </button>
                  ) : (
                    <button onClick={() => handleEdit(Guide)}>Edit</button>
                  )}
                  <button onClick={() => handleDelete(Guide.email)}>
                    Delete
                  </button>
                </td>
                <td>
                  <button onClick={() => toggleShowEventsForGuide(Guide.email)}>
                    {expandedGuides[Guide.email]
                      ? "Hide Events"
                      : "Show Events"}
                  </button>
                </td>
              </tr>
              <JoinedEvents
                expandedGuides={expandedGuides}
                gKey={Guide.email}
                Guide={Guide}
              />
            </React.Fragment>
          ))}
        </tbody>
      </table>
    </>
  );
};

export default GuidesTable;
