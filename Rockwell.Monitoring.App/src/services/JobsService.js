export const fetchJobs = async () => {
    const response = await fetch('https://localhost:5201/Jobs');
    if (!response.ok)
        throw new Error('Error fetching jobs list');
    return await response.json();
}

export const createJob = async (job) => {
    const response = await fetch('https://localhost:5201/Jobs', {
        method: 'POST',
        body: JSON.stringify(job),
        headers: {
            "Content-Type" : "application/json"
        }
    });
    if (!response.ok)
        throw new Error('Error creating job');
        await response.json();
}