import { useUser } from "../../UserContext";
import AdminHeader from "../AdminComponents/AdminHeader";
import UserHeader from "../UserComponents/UserHeader";
const Events = () => {
  const { userRole } = useUser(); // Get userRole from context

  return (
    <>
      <>
        {userRole === "admin" ? (
          <AdminHeader />
        ) : userRole === "user" ? (
          <UserHeader />
        ) : (
          <div>Please log in</div>
        )}
      </>
    </>
  );
};

export default Events;
