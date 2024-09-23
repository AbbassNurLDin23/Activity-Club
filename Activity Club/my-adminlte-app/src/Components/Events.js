import AdminEvents from "./AdminComponents/AdminEvents";
import UserEvents from "./UserComponents/UserEvents";
import { useUser } from "../UserContext";
import Header from "./Common/PHeader";
import Footer from "./Common/Footer";
const Events = () => {
  const { userRole } = useUser(); // Get userRole from context

  return (
    <>
      <Header />
      <main>
        {userRole === "admin" ? (
          <AdminEvents />
        ) : userRole === "user" ? (
          <UserEvents />
        ) : (
          <div>Please log in</div>
        )}
      </main>
      <Footer />
    </>
  );
};

export default Events;
