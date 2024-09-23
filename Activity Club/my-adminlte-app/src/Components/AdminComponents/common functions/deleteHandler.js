import apiClient from "../../Common/authorizedData";

export const deleteHandler = async (
  entityType,
  identifierKey,
  identifierValue,
  setState
) => {
  if (
    window.confirm(
      `Are you sure you want to delete the ${entityType} with ${identifierKey}: ${identifierValue}?`
    )
  ) {
    try {
      if (entityType === "Admin") {
        const url = `https://localhost:7095/api/User/DeleteAdmins?${identifierKey}=${identifierValue}`;
        await apiClient.put(url);
      } else {
        const url = `https://localhost:7095/api/${entityType}/Delete${entityType}?${identifierKey}=${identifierValue}`;
        await apiClient.delete(url);
      }

      // Update the state based on the entity type dynamically
      setState((prev) =>
        prev.filter((item) => item[identifierKey] !== identifierValue)
      );
    } catch (error) {
      console.error(`Error deleting ${entityType}:`, error);
    }
  }
};
