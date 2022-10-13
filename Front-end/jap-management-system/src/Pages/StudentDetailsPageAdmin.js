import { useState, useEffect, useCallback } from 'react';
import { Form, Button } from 'react-bootstrap';
import { useHistory } from 'react-router-dom';

import useQuery from '../Hooks/useQuery';

import StudentForm from '../Components/Students/StudentForm';

import studentIcon from '../Assets/studentIcon.png';

import studentService from '../Services/studentService';
import selectionService from '../Services/selectionService';

import classes from './StudentDetailsPage.module.css';

const StudentDetailsPageAdmin = () => {
  const history = useHistory();

  const [student, setStudent] = useState({});
  const [studentEdit, setStudentEdit] = useState({});
  const INITIAL_COMMENT_STATE = {
    text: '',
    SId: null
  };
  const [comment, setComment] = useState(INITIAL_COMMENT_STATE);
  const [availableSelections, setAvailableSelections] = useState([]);

  const query = useQuery();

  const fetchStudents = useCallback(query => {
    const id = query.get('id');
    studentService
      .fetchStudentById(id)
      .then(response => {
        setStudent(response.data.data);
        setStudentEdit(response.data.data);
      })
      .catch(err => console.log(err));

    selectionService
      .fetchAllSelections()
      .then(response => {
        setAvailableSelections(() => response.data.data);
      })
      .catch(err => console.log(err));
  }, []);

  useEffect(() => {
    fetchStudents(query);
  }, [fetchStudents, query]);

  const handleEditStudent = ev => {
    ev.preventDefault();
    studentService
      .modifyStudent(studentEdit)
      .then(response => {
        setStudent(response.data.data);
        history.push('/');
      })
      .catch(err => {
        console.log(err);
      });
  };

  const handleStudentFormInput = ev => {
    const inputName = ev.target.name;
    const value = ev.target.value;
    setStudentEdit(prevState => {
      let student = {
        ...prevState
      };
      if (inputName === 'firstName') {
        student.firstName = value;
      } else if (inputName === 'lastName') {
        student.lastName = value;
      } else if (inputName === 'email') {
        student.email = value;
      } else if (inputName === 'status') {
        student.status = value;
      } else if (inputName === 'selection') {
        student.selectionId = value;
      }
      return student;
    });
  };

  const handleAddComment = ev => {
    ev.preventDefault();
    studentService
      .addComment(comment)
      .then(comments => {
        setStudent(prevState => {
          const UPDATED_STATE = {
            ...prevState
          };
          UPDATED_STATE.comments = comments.data.data;
          return UPDATED_STATE;
        });
        setComment(INITIAL_COMMENT_STATE);
      })
      .catch(err => console.log(err));
  };

  const handleCommentInput = ev => {
    const value = ev.target.value;
    const id = query.get('id');
    setComment(() => {
      return {
        SId: id,
        text: value,
        createdAt: new Date().toISOString().slice(0, 19)
      };
    });
  };

  return (
    <div className={classes.container}>
      <div className={classes.top}>
        <img src={studentIcon} alt="student" />
        <p>{`${student.firstName} ${student.lastName}`}</p>
      </div>
      <div className={classes.student__data}>
        <h5 style={{ marginTop: 20, marginBottom: 35 }}>Personal data</h5>
        <StudentForm
          formType={'edit'}
          availableSelections={availableSelections}
          handleFormSubmission={handleEditStudent}
          handleStudentFormInput={handleStudentFormInput}
          studentFormData={studentEdit}
        />
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

        <div className={classes.add__comment}>
          <Form onSubmit={handleAddComment}>
            <Form.Group
              className="mb-3"
              controlId="exampleForm.ControlTextarea1"
            >
              <Form.Label>Write a comment:</Form.Label>
              <Form.Control
                value={comment.text}
                onChange={handleCommentInput}
                as="textarea"
                rows={3}
              />
            </Form.Group>
            <Form.Group className="mb-3">
              <Button type="submit" variant="primary">
                Add comment
              </Button>
            </Form.Group>
          </Form>
        </div>
      </div>
      <div className={classes.comments_form}></div>
    </div>
  );
};

export default StudentDetailsPageAdmin;
