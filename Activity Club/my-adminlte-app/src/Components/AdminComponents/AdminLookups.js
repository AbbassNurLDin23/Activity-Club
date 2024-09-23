import apiClient from "../Common/authorizedData";
import React, { useEffect, useState } from "react";
import Header from "../Common/PHeader";
import Footer from "../Common/Footer";
import { deleteHandler } from "./common functions/deleteHandler";
import { fetchDataFromApi } from "./common functions/fetchData";
import LookupsTable from "./common functions/LookupsHandlers/LookupsTable";
import AddLookup from "./common functions/LookupsHandlers/AddLookup";

const AdminLookups = () => {
  const [lookups, setLookups] = useState([]);
  const [isEditing, setIsEditing] = useState(null);
  const [editedLookup, setEditedLookup] = useState({});
  const [isAdding, setIsAdding] = useState(false);
  const [newLookup, setNewLookup] = useState({ code: 0, name: "" });

  useEffect(() => {
    fetchDataFromApi(
      "https://localhost:7095/api/Lookup/GetAllLookups",
      setLookups
    );
  }, [lookups]);

  const handleDelete = async (order) => {
    deleteHandler("lookup", "order", order, setLookups);
  };

  const handleEdit = (lookup) => {
    setIsEditing(lookup.order);
    setEditedLookup({ ...lookup });
  };

  const handleSave = async (order) => {
    try {
      await apiClient.put(
        `https://localhost:7095/api/Lookup/UpdateLookup?order=${order}`,
        editedLookup
      );
      setLookups(
        lookups.map((lookup) =>
          lookup.order === order ? editedLookup : lookup
        )
      );
      setIsEditing(null);
    } catch (error) {
      console.error("Error saving lookup:", error);
    }
  };

  const handleAdd = async () => {
    try {
      await apiClient.post(
        "https://localhost:7095/api/Lookup/CreateLookup",
        newLookup
      );
      setIsAdding(false); // Reset after successful add
      setNewLookup({ code: 0, name: "" }); // Clear input fields after adding
    } catch (error) {
      console.error("Error adding lookup:", error);
    }
  };

  const handleCancel = () => {
    setIsAdding(false);
    setNewLookup({ code: 0, name: "" }); // Reset the input fields when cancelled
  };

  return (
    <>
      <Header />
      <LookupsTable
        lookups={lookups}
        isEditing={isEditing}
        editedLookup={editedLookup}
        setEditedLookup={setEditedLookup}
        handleSave={handleSave}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
      />
      <AddLookup
        isAdding={isAdding}
        newLookup={newLookup}
        setNewLookup={setNewLookup}
        handleAdd={handleAdd}
        handleCancel={handleCancel}
        setIsAdding={setIsAdding}
      />
      <Footer />
    </>
  );
};

export default AdminLookups;
