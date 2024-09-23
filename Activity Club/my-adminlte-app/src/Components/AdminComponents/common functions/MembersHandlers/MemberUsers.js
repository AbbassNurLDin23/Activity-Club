import { useUser } from "../../../../UserContext";
const MemberUsers = ({ toggleShowUsers, showUsers, users }) => {
  const { formatDOB } = useUser();
  return (
    <>
      {/* <h2>Users</h2> */}
      <button onClick={toggleShowUsers}>
        {showUsers ? "Hide Users" : "Show Users"}
      </button>

      {showUsers && (
        <table
          border="1"
          cellPadding="5"
          cellSpacing="0"
          style={{ marginTop: "10px" }}
        >
          <thead>
            <tr>
              <th>Email</th>
              <th>Password</th>
              <th>Name</th>
              <th>Date of Birth</th>
              <th>Gender</th>
              <th>Roles</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.email}>
                <td>{user.email}</td>
                <td>{user.password}</td>
                <td>{user.name}</td>
                <td>{formatDOB(user.dob)}</td>
                <td>{user.gender}</td>
                <td>{user.roles?.join(", ") || "No roles assigned"}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
};
export default MemberUsers;
