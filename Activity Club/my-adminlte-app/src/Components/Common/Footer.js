const Footer = () => {
  return (
    <div className="page-container">
      <div className="content">{/* <!-- Your main content here --> */}</div>
      <footer className="footer">
        <div className="container">
          <div className="row">
            <div className="col text-center">
              <p>&copy; 2024 Activity Club. All rights reserved.</p>
              <p>
                <a href="#" className="footer-link">
                  Privacy Policy
                </a>{" "}
                |{" "}
                <a href="#" className="footer-link">
                  Terms of Service
                </a>
              </p>
              {/* Social media icons */}
              <div className="social-icons">
                <a
                  href="https://www.facebook.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="mx-2"
                >
                  <i className="fab fa-facebook-f"></i>
                </a>
                <a
                  href="https://www.instagram.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="mx-2"
                >
                  <i className="fab fa-instagram"></i>
                </a>
                <a
                  href="https://www.twitter.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="mx-2"
                >
                  <i className="fab fa-twitter"></i>
                </a>
                <a
                  href="https://www.linkedin.com"
                  target="_blank"
                  rel="noopener noreferrer"
                  className="mx-2"
                >
                  <i className="fab fa-linkedin-in"></i>
                </a>
              </div>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
};

export default Footer;
