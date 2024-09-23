import { useState } from "react";

const AddEvent = ({
  eventEmail,
  setEventEmail,
  eventId,
  setEventId,
  handleAddEventToMember,
  toggleShowEvents,
  showEvents,
  events,
}) => {
  const [showAddEventForm, setShowAddEventForm] = useState(false);

  const toggleAddEventForm = () => {
    setShowAddEventForm(!showAddEventForm); // Toggle the form visibility
  };
  return (
    <>
      <div style={{ marginTop: "20px" }}>
        <h3>Add Event to Member</h3>
        <button onClick={toggleAddEventForm}>
          {showAddEventForm ? "Cancel" : "Add Event"}
        </button>
        {showAddEventForm && (
          <div>
            <input
              type="email"
              placeholder="Member Email"
              value={eventEmail}
              onChange={(e) => setEventEmail(e.target.value)}
            />
            <input
              type="text"
              placeholder="Event ID"
              value={eventId}
              onChange={(e) => setEventId(e.target.value)}
            />
            <button onClick={handleAddEventToMember}>Add Event</button>
          </div>
        )}
      </div>

      {/* <h2>Events</h2> */}
      <button onClick={toggleShowEvents}>
        {showEvents ? "Hide Events" : "Show Events"}
      </button>

      {showEvents && (
        <table
          border="1"
          cellPadding="5"
          cellSpacing="0"
          style={{ marginTop: "10px" }}
        >
          <thead>
            <tr>
              <th>Id</th>
              <th>Name</th>
              <th>Description</th>
              <th>Cost</th>
              <th>Destination</th>
              <th>Date From</th>
            </tr>
          </thead>
          <tbody>
            {events.map((event) => (
              <tr key={event.id}>
                <td>{event.id}</td>
                <td>{event.name}</td>
                <td>{event.description}</td>
                <td>{event.cost}</td>
                <td>{event.destination}</td>
                <td>{event.dateFrom}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
};
export default AddEvent;
