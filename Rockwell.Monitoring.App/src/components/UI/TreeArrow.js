import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faAngleRight, faAngleDown } from '@fortawesome/free-solid-svg-icons'

import styles from "./TreeArrow.module.css"

const TreeArrow = (props) => {
    return <button className={styles.treeArrow} onClick={props.onClick}>
        {props.isOpen 
        ? <FontAwesomeIcon icon={faAngleDown}/>
        : <FontAwesomeIcon style={{color:'#cd163f'}} icon={faAngleRight}/>}
    </button>;
};

export default TreeArrow;