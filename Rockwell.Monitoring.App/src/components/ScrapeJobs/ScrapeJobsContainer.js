import Card from "../UI/Card";
import ScrapeJobAdder from "./ScrapeJobAdder";

import styles from './ScrapeJobsContainer.module.css'
import ScrapeJobsTable from "./ScrapeJobsTable";

const ScrapeJobsContainer = () => {
    return <Card className={styles.scrapeJobsContainer}>
        <ScrapeJobAdder></ScrapeJobAdder>
        <ScrapeJobsTable></ScrapeJobsTable>
    </Card>;
}

export default ScrapeJobsContainer;