const AddLookup = ({
  isAdding,
  newLookup,
  setNewLookup,
  handleAdd,
  handleCancel,
  setIsAdding,
}) => {
  return (
    <>
      <h2>Add Lookup</h2>
      {isAdding ? (
        <div>
          <label>Code:</label>
          <input
            type="number"
            value={newLookup.code}
            onChange={(e) =>
              setNewLookup({ ...newLookup, code: parseInt(e.target.value) })
            }
          />
          <br />
          <label>Name:</label>
          <input
            type="text"
            value={newLookup.name}
            onChange={(e) =>
              setNewLookup({ ...newLookup, name: e.target.value })
            }
          />
          <br />
          <button onClick={handleAdd}>Add</button>
          <button onClick={handleCancel}>Cancel</button>
        </div>
      ) : (
        <button onClick={() => setIsAdding(true)}>Add Lookup</button>
      )}
    </>
  );
};
export default AddLookup;
