import { useUser } from "../../../../UserContext";

const MemberEvents = ({ expandedMembers, Member }) => {
  const { formatDOB } = useUser();

  return (
    <>
      {expandedMembers[Member.email] && (
        <tr>
          <td colSpan="12">
            <h4>Events Joined by {Member.email}</h4>
            {expandedMembers[Member.email].length > 0 ? (
              <table
                border="1"
                cellPadding="5"
                cellSpacing="0"
                style={{ marginTop: "10px", width: "100%" }}
              >
                <thead>
                  <tr>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Cost</th>
                    <th>Date From</th>
                  </tr>
                </thead>
                <tbody>
                  {expandedMembers[Member.email].map((event) => (
                    <tr key={event.id}>
                      <td>{event.id}</td>
                      <td>{event.name}</td>
                      <td>{event.description}</td>
                      <td>{event.cost}</td>
                      <td>{formatDOB(event.dateFrom)}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            ) : (
              <p>No events joined by this member.</p>
            )}
          </td>
        </tr>
      )}
    </>
  );
};
export default MemberEvents;
