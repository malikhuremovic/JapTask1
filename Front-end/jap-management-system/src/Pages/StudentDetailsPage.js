import { useState, useEffect, useCallback, useContext } from 'react';
import { Form, Row, Col } from 'react-bootstrap';

import studentService from '../Services/studentService';
import UserContext from '../Store/userContext';

import studentIcon from '../Assets/studentIcon.png';

import classes from './StudentDetailsPage.module.css';

const StudentDetailsPage = () => {
  const [student, setStudent] = useState({});

  const ctx = useContext(UserContext);

  const fetchStudents = useCallback(() => {
    studentService
      .fetchStudentById(ctx.userDataState.id)
      .then(response => {
        setStudent(response.data.data);
      })
      .catch(err => console.log(err));
  }, [ctx]);

  useEffect(() => {
    fetchStudents();
  }, [fetchStudents]);

  return (
    <div className={classes.container}>
      <div className={classes.top}>
        <img src={studentIcon} alt="student" />
        <p>{`${student.firstName} ${student.lastName}`}</p>
      </div>
      <div className={classes.student__data}>
        <h5 style={{ marginTop: 20, marginBottom: 35 }}>Personal data</h5>
        <Form>
          <Form.Group
            as={Row}
            className="mb-3"
            controlId="formHorizontalFirstName"
          >
            <Form.Label column sm={2}>
              First name
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                name="firstName"
                type="text"
                placeholder="Enter first name"
                value={student.firstName}
                disabled={true}
              />
            </Col>
          </Form.Group>
          <Form.Group
            as={Row}
            className="mb-3"
            controlId="formHorizontalLastName"
          >
            <Form.Label column sm={2}>
              Last name
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                name="lastName"
                type="text"
                placeholder="Enter last name"
                value={student.lastName}
                disabled={true}
              />
            </Col>
          </Form.Group>
          <Form.Group as={Row} className="mb-3" controlId="formHorizontalEmail">
            <Form.Label column sm={2}>
              Email
            </Form.Label>
            <Col sm={10}>
              <Form.Control
                name="email"
                type="email"
                placeholder="Enter email"
                value={student.email}
                disabled={true}
              />
            </Col>
          </Form.Group>

          <Form.Group
            as={Row}
            className="mb-3"
            controlId="formHorizontalStudentStatus"
          >
            <Form.Label column sm={2}>
              Student status
            </Form.Label>
            <Col sm={10}>
              <Form.Select
                name="status"
                className="form-select"
                defaultValue={student.status ? student.status : 'none'}
                aria-label="Default select example"
                required
                disabled={true}
              >
                {!student.status && <option value="none">Not Allocated</option>}
                <option value="InProgram">InProgram</option>
                <option value="Success">Success</option>
                <option value="Failed">Failed</option>
                <option value="Extended">Extended</option>
              </Form.Select>
            </Col>
          </Form.Group>
          <Form.Group
            as={Row}
            className="mb-3"
            controlId="formHorizontalSelection"
          >
            <Form.Label column sm={2}>
              Selection
            </Form.Label>
            <Col sm={10}>
              <Form.Select
                name="selection"
                className="form-select"
                defaultValue={
                  student.selection ? student.selection.name : 'none'
                }
                aria-label="Default select example"
                required
                disabled={true}
              >
                {!student.selection && (
                  <option value="none">Not Allocated</option>
                )}
                {student.selection && (
                  <option value={student.selection.name}>
                    {student.selection.name}
                  </option>
                )}
              </Form.Select>
            </Col>
          </Form.Group>
          <Form.Group
            as={Row}
            className="mb-3"
            controlId="formHorizontalSelection"
          >
            <Form.Label column sm={2}>
              Program
            </Form.Label>
            <Col sm={10}>
              <Form.Select
                name="selection"
                className="form-select"
                defaultValue={
                  student.selection ? student.selection.japProgram.name : 'none'
                }
                aria-label="Default select example"
                required
                disabled={true}
              >
                {!student.selection && (
                  <option value="none">Not Allocated</option>
                )}
                {student.selection && (
                  <option value={student.selection.japProgram.name}>
                    {student.selection.japProgram.name}
                  </option>
                )}
              </Form.Select>
            </Col>
          </Form.Group>
        </Form>
      </div>
      <div className={classes.student__comments}>
        <h5 style={{ marginTop: 20, marginBottom: 35 }}>Comments</h5>
        {student.comments &&
          student.comments.map(comment => {
            return (
              <div className={classes.comment_box}>
                <span>
                  Created: {comment.createdAt.split('T').join(' at ')}
                </span>
                <p>{comment.text}</p>
              </div>
            );
          })}
      </div>
      <div className={classes.comments_form}></div>
    </div>
  );
};

export default StudentDetailsPage;
