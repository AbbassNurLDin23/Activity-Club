import React from "react";
import { useUser } from "../../../UserContext";

const TableColumn = ({
  isEditing,
  rowKey,
  type,
  editedValue,
  setEdited,
  edited,
  fieldName,
  fieldValue,
}) => {
  const { formatDOB } = useUser();

  // Check if the field is a date field (dob, dateFrom, or dateTo)
  const isDateField =
    fieldName === "dob" || fieldName === "dateFrom" || fieldName === "dateTo";

  // Format date value for display purposes
  const displayValue = fieldName === "dob" ? formatDOB(fieldValue) : fieldValue;

  return (
    <td>
      {isEditing === rowKey ? (
        <input
          type={isDateField ? "date" : type} // Use "date" input for date fields
          value={
            isDateField
              ? (editedValue || "").slice(0, 10) // Ensure the value is in YYYY-MM-DD format for input type="date"
              : editedValue || ""
          }
          onChange={(e) =>
            setEdited({
              ...edited,
              [fieldName]: e.target.value, // Dynamically update the specific field
            })
          }
        />
      ) : (
        displayValue // Display formatted value for dob or raw value for other fields
      )}
    </td>
  );
};

export default TableColumn;
