import ScrapeJobInfo from "./ScrapeJobInfo";
import styles from "./ScrapeJobsTable.module.css";

import { fetchJobs } from '../../services/JobsService';
import { useCallback, useEffect, useState } from "react";

const ScrapeJobsTable = () => {

    const [jobs, setJobs] = useState([]);
    const [error, setError] = useState(null);

    const fetchJobsHandler = useCallback (async () => {
        try{
            const jobList = await fetchJobs();
            setJobs(jobList);
            setError(null);
        }
        catch (ex){
            setError("Se presentó un error al consultar los Jobs");
        }
    }, []);

    useEffect(() => fetchJobsHandler(), [fetchJobsHandler]);
    
    let rendered;
    if (jobs.length !== 0){
        rendered =jobs.map(job => <ScrapeJobInfo jobInfo={job} key={job.id}></ScrapeJobInfo>);
    }
    else if (error != null){
        rendered = <tr><td colSpan="5">{error}</td></tr>;
    }
    else {
        rendered = <tr><td colSpan="5">No se encontró ningun Job</td></tr>;
    }

    return <table className = {styles.scrapeJobsTable}>
    <thead>
        <tr>
            <th className={styles.viewCol}></th>
            <th className={styles.jobIdCol}>Job Id</th>
            <th className={styles.urlCol}>Url</th>
            <th className={styles.cronCol}>Cron</th>
            <th className={styles.creatiomCol}>Creación</th>
        </tr>
    </thead>
    <tbody>
        {rendered}
    </tbody>
  </table>;
};

export default ScrapeJobsTable;