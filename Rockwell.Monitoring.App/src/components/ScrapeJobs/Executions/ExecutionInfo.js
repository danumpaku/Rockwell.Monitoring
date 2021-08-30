import { useState } from "react";
import TreeArrow from "../../UI/TreeArrow";
import styles from "./ExecutionInfo.module.css";
import { fetchExecutionData } from '../../../services/ExecutionsService';

const ExecutionInfo = (props) => {

    const [executionData, setExecutionData] = useState(null);
    const [error, setError] = useState(null);
    const [isOpen, setIsOpen] = useState(false);

    const openClickHandler = async () => {
        setIsOpen(prev => !prev);
        try{
            const executionObj = await fetchExecutionData(props.execution.executionId);
            setExecutionData(! executionObj.hasErrors ? executionObj.result : executionObj.errorMessage);
            setError(null);
        }
        catch (ex){
            setError("Se presentó un error al consultar el resultado de la ejecución");
        }
    }

    let rendered;
    if (executionData != null){
        rendered = executionData;
    }
    else if (error != null){
        rendered = error;
    }
    else {
        rendered = "Cargando...";
    }

    return <>
        <tr>
            <td><TreeArrow isOpen={isOpen} onClick={openClickHandler}/></td>
            <td>{props.execution.executionTime}</td>
            <td className={props.execution.hasErrors ? styles.execErr : styles.execOk}> Http {props.execution.responseCode}</td>
        </tr>
        {isOpen && <tr>
            <td colSpan="3" className={styles.info}>
                {rendered}
            </td>
        </tr>}
    </>;
}

export default ExecutionInfo;