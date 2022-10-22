import { Form, Row, Col, Button } from 'react-bootstrap';

const StudentDeleteForm = ({ handleDeleteStudent }) => {
  return (
    <Form onSubmit={handleDeleteStudent}>
      <Row className="align-items-center">
        <Col xs="auto">
          <Form.Label>
            Are you aware that by performing this action, student will be
            removed from the JAP platform?
          </Form.Label>
        </Col>
        <Col xs="auto">
          <Button type="submit" variant="danger" className="mb-2">
            I am aware, Delete student
          </Button>
        </Col>
      </Row>
    </Form>
  );
};

export default StudentDeleteForm;
