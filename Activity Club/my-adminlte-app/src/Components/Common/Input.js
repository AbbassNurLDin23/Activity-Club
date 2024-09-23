import React from "react";

const Input = ({ type, value, onChange, placeholder, name, icon, ...rest }) => {
  return (
    <div className="input-group mb-3">
      {icon && (
        <div className="input-group-prepend">
          <span className="input-group-text">
            <i className={icon}></i>
          </span>
        </div>
      )}
      <input
        type={type}
        value={value}
        onChange={onChange}
        placeholder={placeholder}
        name={name}
        className="form-control"
        {...rest}
      />
    </div>
  );
};

export default Input;
