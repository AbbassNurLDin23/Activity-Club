import TableColumn from "../TableColumn";
import React from "react";
import MemberEvents from "./MemberEvents";

const MemberTable = ({
  errorMessage,
  Members,
  editingMember,
  setEditingMember,
  handleSave,
  handleEdit,
  handleDelete,
  toggleShowEventsForMember,
  expandedMembers,
}) => {
  return (
    <>
      <h2>Members</h2>
      {errorMessage && (
        <div style={{ color: "red", marginBottom: "10px" }}>{errorMessage}</div>
      )}
      <table border="1" cellPadding="5" cellSpacing="0">
        <thead>
          <tr>
            <th>Email</th>
            <th>Password</th>
            <th>Name</th>
            <th>Date of Birth</th>
            <th>Gender</th>
            <th>Roles</th>
            <th>Profession</th>
            <th>Mobile Number</th>
            <th>Emergency Number</th>
            <th>Nationality</th>
            <th>Actions</th>
            <th>Show Events</th>
          </tr>
        </thead>
        <tbody>
          {Members.map((Member) => (
            <React.Fragment key={Member.email}>
              <tr>
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.email}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="email"
                  fieldValue={Member.email}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.password}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="password"
                  fieldValue={Member.password}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.name}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="name"
                  fieldValue={Member.name}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="date"
                  editedValue={editingMember.dob}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="dob"
                  fieldValue={Member.dob}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.gender}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="gender"
                  fieldValue={Member.gender}
                />

                <td>{Member.roles?.join(", ") || "No roles assigned"}</td>
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.profession}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="profession"
                  fieldValue={Member.profession}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="number"
                  editedValue={editingMember.mobileNumber}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="mobileNumber"
                  fieldValue={Member.mobileNumber}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="number"
                  editedValue={editingMember.emergencyNumber}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="emergencyNumber"
                  fieldValue={Member.emergencyNumber}
                />
                <TableColumn
                  isEditing={editingMember.email}
                  rowKey={Member.email}
                  type="text"
                  editedValue={editingMember.nationality}
                  setEdited={setEditingMember}
                  edited={editingMember}
                  fieldName="nationality"
                  fieldValue={Member.nationality}
                />

                <td>
                  {editingMember?.email === Member.email ? (
                    <button onClick={() => handleSave(editingMember)}>
                      Save
                    </button>
                  ) : (
                    <button onClick={() => handleEdit(Member)}>Edit</button>
                  )}
                  <button onClick={() => handleDelete(Member.email)}>
                    Delete
                  </button>
                </td>
                {/* New cell for Show Events button */}
                <td>
                  <button
                    onClick={() => toggleShowEventsForMember(Member.email)}
                  >
                    {expandedMembers[Member.email]
                      ? "Hide Events"
                      : "Show Events"}
                  </button>
                </td>
              </tr>
              {/* Conditionally render the events table if the member is expanded */}
              <MemberEvents expandedMembers={expandedMembers} Member={Member} />
            </React.Fragment>
          ))}
        </tbody>
      </table>
    </>
  );
};
export default MemberTable;
