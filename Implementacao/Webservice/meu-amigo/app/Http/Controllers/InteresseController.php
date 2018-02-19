<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Interesse;

class InteresseController extends Controller
{
    public function index()
    {
        return Interesse::all();
    }
}
