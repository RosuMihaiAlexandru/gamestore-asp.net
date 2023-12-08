import React from 'react';

const MyInput = ({ type, placeholder, name, value, onChange, min, max }) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      name={name}
      value={value[name]} 
      onChange={onChange}
      min={min}
      max={max}
      className="form-control"
    />
  );
};

export default MyInput;
