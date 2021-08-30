import React from 'react';

import styles from './Button.module.css';

const Button = (props) => {

  let buttonType = styles.whiteButton;
  if (props.buttonType === 'red')
  buttonType = styles.redButton

  return (
    <button
      className={`${styles.button} ${buttonType}`}
      type={props.type || 'button'}
      onClick={props.onClick}
    >
      {props.children}
    </button>
  );
};

export default Button;
