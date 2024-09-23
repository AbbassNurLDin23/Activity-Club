const JoinedMembers = ({ showMembersId, members }) => {
  return (
    <>
      {showMembersId && members.length > 0 && (
        <div>
          <h3>Members of Event {showMembersId}</h3>
          <table>
            <thead>
              <tr>
                <th>Name</th>
                <th>Email</th>
                <th>Profession</th>
              </tr>
            </thead>
            <tbody>
              {members.map((member) => (
                <tr key={member.email}>
                  <td>{member.name}</td>
                  <td>{member.email}</td>
                  <td>{member.profession}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </>
  );
};
export default JoinedMembers;
