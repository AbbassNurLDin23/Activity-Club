import AdminGuides from "./AdminComponents/AdminGuides";
import UserGuides from "./UserComponents/UserGuides";
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
          <AdminGuides />
        ) : userRole === "user" ? (
          <UserGuides />
        ) : (
          <div>Please log in</div>
        )}
      </main>
      <Footer />
    </>
  );
};

export default Events;
