/*
This code is derived from jgit (http://eclipse.org/jgit).
Copyright owners are documented in jgit's IP log.

This program and the accompanying materials are made available
under the terms of the Eclipse Distribution License v1.0 which
accompanies this distribution, is reproduced below, and is
available at http://www.eclipse.org/org/documents/edl-v10.php

All rights reserved.

Redistribution and use in source and binary forms, with or
without modification, are permitted provided that the following
conditions are met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above
  copyright notice, this list of conditions and the following
  disclaimer in the documentation and/or other materials provided
  with the distribution.

- Neither the name of the Eclipse Foundation, Inc. nor the
  names of its contributors may be used to endorse or promote
  products derived from this software without specific prior
  written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using NGit;
using Sharpen;

namespace NGit
{
	/// <summary>Abstract TreeVisitor for visiting all files known by a Tree.</summary>
	/// <remarks>Abstract TreeVisitor for visiting all files known by a Tree.</remarks>
	[System.ObsoleteAttribute(@"Use NGit.Treewalk.TreeWalk instead, with aNGit.Treewalk.FileTreeIterator as one of its members."
		)]
	public abstract class TreeVisitorWithCurrentDirectory : TreeVisitor
	{
		private readonly AList<FilePath> stack = new AList<FilePath>(16);

		private FilePath currentDirectory;

		internal TreeVisitorWithCurrentDirectory(FilePath rootDirectory)
		{
			currentDirectory = rootDirectory;
		}

		internal virtual FilePath GetCurrentDirectory()
		{
			return currentDirectory;
		}

		/// <exception cref="System.IO.IOException"></exception>
		public virtual void StartVisitTree(Tree t)
		{
			stack.AddItem(currentDirectory);
			if (!t.IsRoot())
			{
				currentDirectory = new FilePath(currentDirectory, t.GetName());
			}
		}

		/// <exception cref="System.IO.IOException"></exception>
		public virtual void EndVisitTree(Tree t)
		{
			currentDirectory = stack.Remove(stack.Count - 1);
		}

		public abstract void VisitFile(FileTreeEntry arg1);

		public abstract void VisitGitlink(GitlinkTreeEntry arg1);

		public abstract void VisitSymlink(SymlinkTreeEntry arg1);
	}
}
