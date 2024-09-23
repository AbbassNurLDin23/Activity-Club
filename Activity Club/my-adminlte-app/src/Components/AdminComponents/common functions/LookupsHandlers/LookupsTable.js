import TableColumn from "../TableColumn";

const LookupsTable = ({
  lookups,
  isEditing,
  editedLookup,
  setEditedLookup,
  handleSave,
  handleEdit,
  handleDelete,
}) => {
  return (
    <>
      <h2>Lookups</h2>
      <table>
        <thead>
          <tr>
            <th>Order</th>
            <th>Code</th>
            <th>Name</th>
          </tr>
        </thead>
        <tbody>
          {lookups.map((lookup) => (
            <tr key={lookup.order}>
              <TableColumn
                isEditing={isEditing}
                rowKey={lookup.order}
                type="number"
                editedValue={editedLookup.order}
                setEdited={setEditedLookup}
                edited={editedLookup}
                fieldName="order"
                fieldValue={lookup.order}
              />
              <TableColumn
                isEditing={isEditing}
                rowKey={lookup.order}
                type="number"
                editedValue={editedLookup.code}
                setEdited={setEditedLookup}
                edited={editedLookup}
                fieldName="code"
                fieldValue={lookup.code}
              />
              <TableColumn
                isEditing={isEditing}
                rowKey={lookup.order}
                type="name"
                editedValue={editedLookup.name}
                setEdited={setEditedLookup}
                edited={editedLookup}
                fieldName="name"
                fieldValue={lookup.name}
              />

              <td>
                {isEditing === lookup.order ? (
                  <button onClick={() => handleSave(lookup.order)}>Save</button>
                ) : (
                  <>
                    <button onClick={() => handleEdit(lookup)}>Edit</button>
                    <button onClick={() => handleDelete(lookup.order)}>
                      Delete
                    </button>
                  </>
                )}
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </>
  );
};
export default LookupsTable;
