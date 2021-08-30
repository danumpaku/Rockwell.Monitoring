import Button from "../UI/Button";
import styles from "./ScrapeJobAdder.module.css"
import { createJob } from '../../services/JobsService';
import { useState } from "react";

const ScrapeJobAdder = () => {

    const [url, setUrl] = useState("");
    const [cronExpression, setCron] = useState("");

    const submitHandler = async(event) => {
        event.preventDefault();
        await createJob({
            url,
            cronExpression
        });
        setUrl("");
        setCron("");
    };

    const urlChangedHandler = (event) => setUrl(event.target.value);
    const cronChangedHandler = (event) => setCron(event.target.value);

    return <form className={styles.adderForm} onSubmit={submitHandler}>
        <div className={`${styles.formField} ${styles.urlField}`}>
            <label htmlFor="url">Url</label>
            <input id="url" type="text" value={url} onChange={urlChangedHandler}></input>
        </div>
        <div className={`${styles.formField} ${styles.cronField}`}>
            <label htmlFor="cron">Cron</label>
            <input id="cron" type="text" value={cronExpression} onChange={cronChangedHandler}></input>
        </div>
        <Button type='submit'>Agregar</Button>
    </form>
}

export default ScrapeJobAdder;