import { useState } from "react";
import ExecutionInfo from "./ExecutionInfo/ExecutionInfo";
import TreeArrow from "../../UI/TreeArrow";
import { fetchExecutions } from '../../../services/ExecutionsService';

const ScrapeJobInfo = (props) => {

    const [executions, setExecutions] = useState([]);
    const [error, setError] = useState(null);
    const [isOpen, setIsOpen] = useState(false);

    const openClickHandler = async () => {
        setIsOpen(prev => !prev);
        try{
          const executionList = await fetchExecutions(props.jobInfo.id);
          setExecutions(executionList);
          setError(null);
        }
        catch (ex){
            setError("Se presentó un error al consultar las ejecuciones del Job");
        }
    }

    let rendered;
    if (executions.length !== 0){
        rendered = <td colSpan="5">
            <table>
              <tbody>
                {executions.map (exec => <ExecutionInfo execution={exec} key={exec.executionId}/>)}
              </tbody>
            </table>
          </td>;
    }
    else if (error != null){
        rendered = <td colSpan="5">{error}</td>;
    }
    else {
        rendered = <td colSpan="5">No se encontró ninguna ejecución del Job</td>;
    }

    return <>
            <tr>
                <td><TreeArrow isOpen={isOpen} onClick={openClickHandler}/></td>
                <td>{props.jobInfo.id}</td>
                <td>{props.jobInfo.url}</td>
                <td>{props.jobInfo.cronExpression}</td>
                <td>{props.jobInfo.creationTime}</td>
            </tr>
            {isOpen && <tr>
                {rendered}
            </tr>}
        </>;
};

export default ScrapeJobInfo;