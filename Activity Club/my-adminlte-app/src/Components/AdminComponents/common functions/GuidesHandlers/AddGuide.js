const AddGuide = ({
  toggleAddGuideForm,
  showAddGuideForm,
  newGuideEmail,
  setNewGuideEmail,
  handleAddGuide,
  newGuideProfession,
  setNewGuideProfession,
}) => {
  return (
    <div>
      <button onClick={toggleAddGuideForm}>
        {showAddGuideForm ? "Cancel" : "Add Guide"}
      </button>

      {showAddGuideForm && (
        <div>
          {/* <h3>Add New Guide</h3> */}
          <input
            type="email"
            placeholder="Guide Email"
            value={newGuideEmail}
            onChange={(e) => setNewGuideEmail(e.target.value)}
          />
          <input
            type="text"
            placeholder="Profession"
            value={newGuideProfession}
            onChange={(e) => setNewGuideProfession(e.target.value)}
          />

          <button onClick={handleAddGuide}>Add Guide</button>
        </div>
      )}
    </div>
  );
};
export default AddGuide;
