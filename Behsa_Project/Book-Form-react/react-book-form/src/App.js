import logo from './logo.svg';
import './App.css';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import { Container, Navbar, Nav } from 'react-bootstrap';
import Landing from './pages/landing';
import Writers from './pages/writers';
import EditBook from './components/edit-book';
import BookDetails from './components/book-details';
import CreateEditWriter from './components/create-edit-writer'
import WriterDetail from './components/writer-details';


function App() {
  return (
    <Container>
      <BrowserRouter>
        <Navbar bg="dark" variant="dark">
          <Navbar.Brand as={Link} to="/">Book World</Navbar.Brand>
          <Nav className="mr-auto">
            <Nav.Link as={Link} to="/">Books</Nav.Link>
            <Nav.Link as={Link} to="/writers">Writers</Nav.Link>
          </Nav>
        </Navbar>
        <Routes>
          <Route exact path='/' Component={() => <Landing />} />
          <Route exact path='/details/:bookid' Component={BookDetails} />
          <Route exact path='/edit/:bookid' Component={EditBook} />
          <Route exact path='/writers' Component={Writers} />
          <Route exact path='/writers/Create-edit' Component={CreateEditWriter} />
          <Route exact path='/writers/details/:writerid' Component={WriterDetail} />

        </Routes>
      </BrowserRouter>
    </Container>
  );
}

export default App;
