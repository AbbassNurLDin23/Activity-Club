const AddMember = ({
  toggleAddMemberForm,
  showAddMemberForm,
  newMemberEmail,
  setNewMemberEmail,
  newMemberProfession,
  setNewMemberProfession,
  newMemberMN,
  setNewMemberMN,
  newMemberEN,
  setNewMemberEN,
  newMemberNationality,
  setNewMemberNationality,
  handleAddMember,
}) => {
  return (
    <div>
      <button onClick={toggleAddMemberForm}>
        {showAddMemberForm ? "Cancel" : "Add Member"}
      </button>

      {showAddMemberForm && (
        <div style={{ marginTop: "20px" }}>
          {/* <h3>Add New Member</h3> */}
          <input
            type="email"
            placeholder="Member Email"
            value={newMemberEmail}
            onChange={(e) => setNewMemberEmail(e.target.value)}
          />
          <input
            type="text"
            placeholder="Profession"
            value={newMemberProfession}
            onChange={(e) => setNewMemberProfession(e.target.value)}
          />
          <input
            type="number"
            placeholder="Mobile Number"
            value={newMemberMN}
            onChange={(e) => setNewMemberMN(e.target.value)}
          />
          <input
            type="number"
            placeholder="Emergency Number"
            value={newMemberEN}
            onChange={(e) => setNewMemberEN(e.target.value)}
          />
          <input
            type="text"
            placeholder="Nationality"
            value={newMemberNationality}
            onChange={(e) => setNewMemberNationality(e.target.value)}
          />
          <button onClick={handleAddMember}>Add Member</button>
        </div>
      )}
    </div>
  );
};
export default AddMember;
