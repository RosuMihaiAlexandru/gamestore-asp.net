import React from "react";

const MyCreateInput = ({
  type,
  placeholder,
  name,
  value,
  onChange,
  min,
  max,
}) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      name={name}
      value={value}
      onChange={(e) => onChange(e, name)}
      min={min}
      max={max}
      className={type === "file" ? "form-control-file" : "form-control"}
    />
  );
};

export default MyCreateInput;
