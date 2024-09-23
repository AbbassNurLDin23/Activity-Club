const AvailableGuides = ({ showTable, addingGuideId, guideData }) => {
  return (
    <>
      {showTable && addingGuideId && (
        <div>
          <h2>Available Guides</h2>
          <table>
            <thead>
              <tr>
                {/* Adjust headers based on your data */}
                <th>Email</th>
                <th>Name</th>
                <th>Profession</th>
              </tr>
            </thead>
            <tbody>
              {guideData.map((item) => (
                <tr key={item.email}>
                  <td>{item.email}</td>
                  <td>{item.name}</td>
                  <td>{item.profession}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </>
  );
};
export default AvailableGuides;
