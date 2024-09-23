import apiClient from "../../Common/authorizedData";

export const fetchDataFromApi = async (endpoint, setterFunction) => {
  try {
    const response = await apiClient.get(endpoint);
    const data = await response.data;
    setterFunction(data || []); // Ensure the state is always set to an array
  } catch (error) {
    console.error(`Error fetching data from ${endpoint}:`, error);
  }
};
