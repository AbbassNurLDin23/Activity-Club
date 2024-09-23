// UserContext.js
import React, { createContext, useState, useContext } from "react";

const UserContext = createContext();

export const UserProvider = ({ children }) => {
  const [userRole, setUserRole] = useState(null);
  const [userEmail, setUserEmail] = useState("");
  const [userType, setUserType] = useState("");
  const formatDOB = (dob) => {
    const dateObject = new Date(dob);
    return isNaN(dateObject.getTime())
      ? "Not provided"
      : dateObject.toLocaleDateString();
  };
  return (
    <UserContext.Provider
      value={{
        userRole,
        setUserRole,
        userEmail,
        setUserEmail,
        userType,
        setUserType,
        formatDOB,
      }}
    >
      {children}
    </UserContext.Provider>
  );
};

export const useUser = () => useContext(UserContext);
