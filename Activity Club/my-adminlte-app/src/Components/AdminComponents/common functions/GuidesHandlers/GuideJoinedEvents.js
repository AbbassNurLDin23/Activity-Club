import { useUser } from "../../../../UserContext";

const JoinedEvents = ({ expandedGuides, gKey, Guide }) => {
  const { formatDOB } = useUser();
  return (
    <>
      {expandedGuides[Guide.email] && (
        <tr>
          <td colSpan="12">
            <h4>Events Joined by {gKey}</h4>
            {expandedGuides[Guide.email].length > 0 ? (
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
                  {expandedGuides[gKey].map((event) => (
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
              <p>No events joined by this Guide.</p>
            )}
          </td>
        </tr>
      )}
    </>
  );
};
export default JoinedEvents;
