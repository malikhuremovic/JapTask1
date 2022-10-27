import React from 'react';
import { Button } from 'react-bootstrap';

import classes from '../Style/Table.module.css';

const PersonalReportTable = ({ personalReport }) => {
  return (
    <React.Fragment>
      <div
        style={{
          backgroundColor: `${'#fff'}`,
          borderRadius: '20px',
          padding: '20px',
          margin: '20px'
        }}
      >
        <div className="table__section table-responsive">
          <div
            style={{
              display: 'flex',
              flexDirection: 'row',
              justifyContent: 'space-between',
              padding: '20px'
            }}
          ></div>
          <table className="table table-striped">
            <caption>Personal student report</caption>
            <thead>
              <tr>
                <th scope="col">#Order</th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Name:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Description:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>URLs:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Expected hours:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Date Start:</span>
                  </div>
                </th>
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Date End:</span>
                  </div>
                </th>{' '}
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Status:</span>
                  </div>
                </th>{' '}
                <th scope="col">
                  <div className={classes.column__Title__Sort}>
                    &nbsp; <span>Done %:</span>
                  </div>
                </th>
              </tr>
            </thead>
            <tbody>
              {personalReport &&
                personalReport.map((s, index) => {
                  let boldClass = s.isEvent ? classes.bold : '';
                  return (
                    <tr key={index}>
                      <th scope="row">
                        <Button
                          style={{ minWidth: 40 }}
                          variant="success"
                          disabled
                        >
                          <span style={{ fontSize: 16 }}>{index + 1}</span>
                        </Button>
                      </th>
                      <td className={boldClass}>{s.name}</td>
                      <td>{s.description}</td>
                      <td>
                        {s.url.split(',').map(url => {
                          return (
                            <a
                              style={{
                                textDecoration: 'none',
                                marginRight: 10
                              }}
                              href={url.trim()}
                            >
                              <Button variant="outline-primary">Link</Button>
                            </a>
                          );
                        })}
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 50 }}
                          disabled
                          variant="outline-success"
                        >
                          {s.expectedHours}h
                        </Button>
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 100 }}
                          variant="success"
                          disabled
                        >
                          {s.dateStart.split('T')[0]}{' '}
                        </Button>
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 100 }}
                          variant="warning"
                          disabled
                        >
                          {s.dateEnd.split('T')[0]}{' '}
                        </Button>
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 100 }}
                          variant="danger"
                          disabled
                        >
                          {s.status}{' '}
                        </Button>
                      </td>
                      <td>
                        <Button
                          style={{ minWidth: 100 }}
                          variant="danger"
                          disabled
                        >
                          {s.done}{' '}
                        </Button>
                      </td>
                    </tr>
                  );
                })}
            </tbody>
          </table>
        </div>
      </div>
    </React.Fragment>
  );
};

export default PersonalReportTable;
