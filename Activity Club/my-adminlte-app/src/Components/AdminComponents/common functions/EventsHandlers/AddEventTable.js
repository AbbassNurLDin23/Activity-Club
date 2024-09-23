const AddEventTable = ({
  isAdding,
  newEvent,
  setNewEvent,
  setIsAdding,
  handleAdd,
  handleCancel,
}) => {
  return (
    <>
      <h2>Add Event</h2>
      {isAdding ? (
        <div>
          <label>Name:</label>
          <input
            type="text"
            value={newEvent.name}
            onChange={(e) => setNewEvent({ ...newEvent, name: e.target.value })}
          />
          <br />
          <label>Description:</label>
          <input
            type="text"
            value={newEvent.description}
            onChange={(e) =>
              setNewEvent({ ...newEvent, description: e.target.value })
            }
          />
          <br />
          <label>Cost:</label>
          <input
            type="number"
            value={newEvent.cost}
            onChange={(e) => setNewEvent({ ...newEvent, cost: e.target.value })}
          />
          <br />
          <label>Status:</label>
          <input
            type="text"
            value={newEvent.status}
            onChange={(e) =>
              setNewEvent({ ...newEvent, status: e.target.value })
            }
          />
          <br />
          <label>Destination:</label>
          <input
            type="text"
            value={newEvent.destination}
            onChange={(e) =>
              setNewEvent({ ...newEvent, destination: e.target.value })
            }
          />
          <br />
          <label>Category:</label>
          <input
            type="number"
            value={newEvent.category}
            onChange={(e) =>
              setNewEvent({ ...newEvent, category: e.target.value })
            }
          />
          <br />
          <label>Date From:</label>
          <input
            type="date"
            value={newEvent.dateFrom}
            onChange={(e) =>
              setNewEvent({ ...newEvent, dateFrom: e.target.value })
            }
          />
          <br />
          <label>Date To:</label>
          <input
            type="date"
            value={newEvent.dateTo}
            onChange={(e) =>
              setNewEvent({ ...newEvent, dateTo: e.target.value })
            }
          />
          <br />
          <button onClick={handleAdd}>Add</button>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      ) : (
        <button onClick={() => setIsAdding(true)}>Add New Event</button>
      )}
    </>
  );
};
export default AddEventTable;
