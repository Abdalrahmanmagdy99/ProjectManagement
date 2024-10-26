import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css'; 
import 'bootstrap-icons/font/bootstrap-icons.css'; 

function App() {
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    fetch('https://localhost:7026/api/Project') 
      .then((response) => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      })
      .then((data) => {
        setProjects(data);
        setLoading(false);
      })
      .catch((error) => {
        setError(error.message);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <div className="container mt-5 text-center">Loading...</div>;
  }

  if (error) {
    return <div className="container mt-5 text-center">Error: {error}</div>;
  }

  return (
    <div className="container mt-5">
      <h1 className="text-center mb-4">Projects List</h1>
      {projects.length > 0 ? (
        projects.map((project, index) => (
          <div key={index} className="card mb-4 shadow-sm">
            <div className="card-header bg-primary text-white">
              <h2 className="card-title mb-0">{project.name}</h2>
            </div>
            <div className="card-body">
              <p className="card-text"><strong>Description:</strong> {project.description}</p>
              <p className="card-text">
                <i className="bi bi-calendar2-check"></i> <strong>Start Date:</strong> {new Date(project.startDate).toLocaleDateString()}
              </p>
              <p className="card-text">
                <i className="bi bi-calendar2-x"></i> <strong>End Date:</strong> {new Date(project.endDate).toLocaleDateString()}
              </p>
              <p className="card-text">
                <i className="bi bi-currency-dollar"></i> <strong>Budget:</strong> ${project.budget.toFixed(2)}
              </p>
              <p className="card-text">
                <i className="bi bi-person-fill"></i> <strong>Owner:</strong> {project.owner}
              </p>
              <p className="card-text">
                <i className="bi bi-flag-fill"></i> <strong>Status:</strong> {project.status}
              </p>

              <h3 className="mt-4">Tasks</h3>
              {project.tasks.length > 0 ? (
                <div className="row">
                  {project.tasks.map((task) => (
                    <div key={task.id} className="col-md-6 mb-3">
                      <div className="card task-card p-3">
                        <h5 className="task-title mb-2">{task.name}</h5>
                        <p><strong>Description:</strong> {task.description}</p>
                        <p><strong>Assigned To:</strong> {task.assignedTo}</p>
                        <p><strong>Priority:</strong> {task.priority}</p>
                        <p><strong>Status:</strong> {task.status}</p>
                        <p><strong>Start Date:</strong> {new Date(task.startDate).toLocaleDateString()}</p>
                        <p><strong>End Date:</strong> {new Date(task.endDate).toLocaleDateString()}</p>
                      </div>
                    </div>
                  ))}
                </div>
              ) : (
                <p className="text-muted">No tasks found for this project.</p>
              )}
            </div>
          </div>
        ))
      ) : (
        <p className="text-center">No projects found.</p>
      )}
    </div>
  );
}

export default App;
