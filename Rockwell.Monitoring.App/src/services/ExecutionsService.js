const fetchExecutions = async (jobId) => {
    const response = await fetch(`https://localhost:5201/Executions/job/${jobId}`);
    if (!response.ok)
        throw new Error(`Error fetching executions for job ${jobId}`);
    return await response.json();
}

const fetchExecutionData = async (executionId) => {
    const response = await fetch(`https://localhost:5201/Executions/${executionId}`);
    if (!response.ok)
        throw new Error(`Error fetching execution info for job ${executionId}`);
    return await response.json();
}

export {fetchExecutions, fetchExecutionData}