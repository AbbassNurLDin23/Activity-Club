import React from "react";
import Input from "./Input"; // Adjust path as necessary

const Form = ({ onSubmit, inputs, buttonText, action }) => {
  return (
    <form onSubmit={onSubmit} action={action}>
      {inputs.map((input, index) => (
        <Input
          key={index}
          type={input.type}
          value={input.value}
          onChange={input.onChange}
          placeholder={input.placeholder}
          name={input.name}
          icon={input.icon} // Pass the icon prop
        />
      ))}
      <div className="input-group mb-3">
        <button type="submit" className="btn btn-primary">
          {buttonText}
        </button>
      </div>
    </form>
  );
};

export default Form;
