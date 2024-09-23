import TableColumn from "../TableColumn";

const EventsTable = ({
  events,
  isEditing,
  setEditedEvent,
  editedEvent,
  handleSave,
  handleEdit,
  handleDelete,
  handleToggleMembers,
  showMembersId,
  setAddingGuideId,
  addingGuideId,
  fetchGuideData,
  setGuideEmail,
  handleAddGuide,
  guideEmail,
}) => {
  return (
    <>
      <h2>Events</h2>
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
            <th>Actions</th>
            <th>Members</th>
            <th>Guides</th>
          </tr>
        </thead>
        <tbody>
          {events && events.length > 0 ? (
            events.map((event) => (
              <tr key={event.id}>
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="text"
                  editedValue={editedEvent.name}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="name"
                  fieldValue={event.name}
                />

                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="text"
                  editedValue={editedEvent.description}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="description"
                  fieldValue={event.description}
                />

                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="number"
                  editedValue={editedEvent.cost}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="cost"
                  fieldValue={event.cost}
                />
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="text"
                  editedValue={editedEvent.status}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="status"
                  fieldValue={event.status}
                />
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="text"
                  editedValue={editedEvent.destination}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="destination"
                  fieldValue={event.destination}
                />
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="number"
                  editedValue={editedEvent.category}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="category"
                  fieldValue={event.category}
                />
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="date"
                  editedValue={editedEvent.dateFrom}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="dateFrom"
                  fieldValue={event.dateFrom}
                />
                <TableColumn
                  isEditing={isEditing}
                  rowKey={event.id}
                  type="date"
                  editedValue={editedEvent.dateTo}
                  setEdited={setEditedEvent}
                  edited={editedEvent}
                  fieldName="dateTo"
                  fieldValue={event.dateTo}
                />

                <td>
                  {isEditing === event.id ? (
                    <button onClick={() => handleSave(event.id)}>Save</button>
                  ) : (
                    <>
                      <button onClick={() => handleEdit(event)}>Edit</button>
                      <button onClick={() => handleDelete(event.id)}>
                        Delete
                      </button>
                    </>
                  )}
                </td>
                <td>
                  <button onClick={() => handleToggleMembers(event.id)}>
                    {showMembersId === event.id
                      ? "Hide Members"
                      : "Show Members"}
                  </button>
                </td>
                <td>
                  <button
                    onClick={() => {
                      setAddingGuideId(
                        addingGuideId === event.id ? null : event.id
                      );
                      fetchGuideData(event.id);
                    }}
                  >
                    {addingGuideId === event.id ? "Cancel" : "Add Guide"}
                  </button>
                  {addingGuideId === event.id && (
                    <div>
                      <input
                        type="email"
                        placeholder="Enter guide email"
                        value={guideEmail}
                        onChange={(e) => setGuideEmail(e.target.value)}
                      />
                      <button onClick={() => handleAddGuide(event.id)}>
                        Submit
                      </button>
                    </div>
                  )}
                </td>
              </tr>
            ))
          ) : (
            <tr>
              <td colSpan="10">No events found.</td>
            </tr>
          )}
        </tbody>
      </table>
    </>
  );
};
export default EventsTable;
