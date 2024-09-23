import React, { useState, useEffect } from "react";
import { useUser } from "../UserContext";
import Header from "./Common/PHeader";
import Footer from "./Common/Footer";

const Home = () => {
  const { userRole } = useUser(); // Get userRole from context

  const sliderImages = [
    "images/ss.jpeg",
    "/images/OIP (1).jpeg",
    "/images/OIP (2).jpeg",
  ];

  const [currentImageIndex, setCurrentImageIndex] = useState(0);

  useEffect(() => {
    const interval = setInterval(() => {
      setCurrentImageIndex(
        (prevIndex) => (prevIndex + 1) % sliderImages.length
      );
    }, 3000);

    return () => clearInterval(interval);
  }, [sliderImages.length]);

  const sectionImages1 = [
    { url: "/images/OIP (3).jpeg" },
    { url: "/images/OIP (4).jpeg" },
  ];

  const sectionImages2 = [
    { url: "/images/OIP.jpeg" },
    { url: "/images/download.jpeg" },
  ];

  return (
    <>
      <Header />
      <main className="home-container">
        <section className="intro-section">
          <h1>Welcome to Our Activity Club</h1>
          <p>
            Discover a vibrant community where you can engage in various
            activities designed to enrich your life. Our club offers a wide
            range of events and programs tailored to suit your interests and
            passions.
          </p>
          <p>
            Whether you're looking to enhance your skills, meet new people, or
            simply have fun, we have something for everyone. Join us and become
            part of a supportive and dynamic environment that fosters personal
            growth and enjoyment.
          </p>
        </section>

        <section className="slider-section">
          <h2>Featured Activity</h2>
          <div className="slider">
            <img
              src={sliderImages[currentImageIndex]}
              alt="Slider Activity"
              className="slider-image"
            />
          </div>
        </section>

        <section className="photo-section">
          <h2>Our Exciting Activities</h2>
          <p>
            Explore some of the most popular activities we offer. Each event is
            carefully curated to provide you with an enjoyable and rewarding
            experience. Check out our latest activities below.
          </p>
          <div className="image-grid">
            {sectionImages1.map((image, index) => (
              <div key={index} className="image-item">
                <img src={image.url} alt={image.alt} />
                <p>{image.alt}</p>
              </div>
            ))}
          </div>
        </section>

        <section className="photo-section">
          <h2>Explore More</h2>
          <p>
            Delve deeper into our wide array of activities. From exciting
            workshops to engaging social events, we have something to offer for
            everyone. Discover more about our upcoming events and get involved.
          </p>
          <div className="image-grid">
            {sectionImages2.map((image, index) => (
              <div key={index} className="image-item">
                <img src={image.url} alt={image.alt} />
                <p>{image.alt}</p>
              </div>
            ))}
          </div>
        </section>
      </main>
      <Footer />
    </>
  );
};

export default Home;
