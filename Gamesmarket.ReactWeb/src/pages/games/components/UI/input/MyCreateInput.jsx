import React from 'react';

const MyCreateInput = ({ type, placeholder, name, value, onChange, min, max }) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      name={name}
      value={value} 
      onChange={onChange}
      min={min}
      max={max}
      className="form-control"
    />
  );
};

export default MyCreateInput;
