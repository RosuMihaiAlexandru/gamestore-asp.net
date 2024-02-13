import React from "react";

const ModalSelect = ({ options, id, name, value, onChange }) => {
  return (
    <select
      id={id}
      name={name}
      value={value[name]}
      onChange={onChange}
      className="form-control"
    >
      {options.map((option) => (
        <option key={option.value} value={option.value}>
          {option.label}
        </option>
      ))}
    </select>
  );
};

export default ModalSelect;
